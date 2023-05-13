using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<bool>> SignIn(SignInInput signInInput)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            }); // identityserver'daki tüm endpointler'i verir.

            if (disco.IsError)
                throw disco.Exception;

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                UserName = signInInput.Email,
                Password = signInInput.Password,
                Address = disco.TokenEndpoint
            }; //token almak için ilgili bilgiler girildi.(password grand type)

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest); //token isteği yapıldı.

            if (token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();

                var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Response<bool>.Fail(errorDto.Errors, 400); 
            }

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = disco.UserInfoEndpoint
            };

            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                throw userInfo.Exception;
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role"); //oluşacak cookie'nin kimliği belirlendi.

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity); //cookie'nin temeli oluşturuldu.

            var authenticationProperties = new AuthenticationProperties();

            //refresh token ve access token cookie'de tutuldu.
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                 new AuthenticationToken{Name=OpenIdConnectParameterNames.AccessToken,Value = token.AccessToken},
                 new AuthenticationToken{Name=OpenIdConnectParameterNames.RefreshToken,Value = token.RefreshToken},

                 new AuthenticationToken{Name=OpenIdConnectParameterNames.ExpiresIn,Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });

            authenticationProperties.IsPersistent = signInInput.IsRemember; //oluşacak cookie'nin kalıcı olup olmadığı belirlendi.

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties); //artık cookie oluşturuldu.

            return Response<bool>.Success(200);
        }
    }
}
