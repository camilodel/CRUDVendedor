using SellerCRUD.Services.Common;
using SellerCRUD.Services.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Services.Interfaces
{
    public interface ISellerService
    {
        Task<ServiceResponse<List<SellerDto>>> GetSellersAsync();
        Task<ServiceResponse<SellerDto>> CreateSellerAsync(CreateSellerDto createSeller);
        Task<ServiceResponse<SellerDto>> UpdateSellerAsync(int id, SellerDto seller);
    }
}
