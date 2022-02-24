using SellerCRUD.Domain.Common;

namespace SellerCRUD.Domain.Entities
{
    public class Seller : AuditEntity 
    {
        public Seller() { }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int IdentificationNumber { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
