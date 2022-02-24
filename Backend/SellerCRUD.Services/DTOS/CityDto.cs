using System.Collections.Generic;

namespace SellerCRUD.Services.DTOS
{
    public class CityDto
    {
        public CityDto() { }
        public int Id { get; set; }
        public string Description { get; set; }
        public List<SellerDto> Sellers { get; set; }
    }
}
