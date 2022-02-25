using SellerCRUD.Services.Common;
using SellerCRUD.Services.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Services.Interfaces
{
    public interface ICityService
    {
        Task<ServiceResponse<List<CityDto>>> GetCitiesAsync();
        Task<ServiceResponse<CityDto>> CreateCityAsync(CreateCityDto createCity);
        Task<ServiceResponse<CityDto>> UpdateCityAsync(int id, CityDto seller);
        Task<ServiceResponse<List<CityDto>>> DeleteCitiesAsync(IEnumerable<int> ids);
    }
}
