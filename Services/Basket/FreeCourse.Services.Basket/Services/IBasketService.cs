using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string UserId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string UserId);
    }
}
