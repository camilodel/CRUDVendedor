using SellerCRUD.Services.DTOS;

namespace SellerCRUD
{
    public class SellerProfile : AutoMapper.Profile
    {
        public SellerProfile()
        {
            CreateMap<Domain.Entities.Seller, SellerDto>();
            CreateMap<CreateSellerDto, Domain.Entities.Seller>();
            CreateMap<SellerDto, Domain.Entities.Seller>();
        }
    }
}
