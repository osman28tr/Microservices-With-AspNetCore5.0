using FreeCourse.Web.Models.PhotoStock;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
	public class PhotoStockService : IPhotoStockService
	{
		private readonly HttpClient _httpClient;
        public PhotoStockService(HttpClient httpClient)
        {
			_httpClient = httpClient;
        }
        public async Task<bool> DeletePhoto(string photoUrl)
		{
			var response = await _httpClient.DeleteAsync($"photo?photoUrl={photoUrl}");
			return response.IsSuccessStatusCode;
		}

		public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
		{
			if (photo == null || photo.Length <= 0)
				return null;
			var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
		    using var memoryStream = new MemoryStream();
			await photo.CopyToAsync(memoryStream);
			var multipartContent = new MultipartFormDataContent();
			multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "photo", randomFileName); //photocontroller -> savephoto(ıformfile photo) parametresine karşılık gelir. verilen photo ismi ile aynı olmalı.
			var response = await _httpClient.PostAsync("photo", multipartContent);
			if(!response.IsSuccessStatusCode)
				return null;
			return await response.Content.ReadFromJsonAsync<PhotoViewModel>();
		}
	}
}
