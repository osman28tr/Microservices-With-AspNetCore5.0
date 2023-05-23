using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IClientCredentialsTokenService
    {
        Task<string> GetToken();
    }
}
