using SellerCRUD.Services.DTOS;

namespace SellerCRUD
{
    public class CityProfile : AutoMapper.Profile
    {
        public CityProfile()
        {
            CreateMap<Domain.Entities.City, CityDto>();
            CreateMap<CreateCityDto, Domain.Entities.City>();
        }
    }
}
