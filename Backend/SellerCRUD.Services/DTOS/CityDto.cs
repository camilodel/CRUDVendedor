using SellerCRUD.Domain.Entities;
using System.Collections.Generic;

namespace SellerCRUD.Services.DTOS
{
    public class CityDto
    {
        public string Description { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}
