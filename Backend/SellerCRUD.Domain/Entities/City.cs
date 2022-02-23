using SellerCRUD.Domain.Common;
using System.Collections.Generic;

namespace SellerCRUD.Domain.Entities
{
    public class City : AuditEntity
    {
        public string Description { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}
