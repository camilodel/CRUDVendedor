namespace SellerCRUD.Services.DTOS
{
    public class SellerDto
    {
        public SellerDto() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int IdentificationNumber { get; set; }
        public int CityId { get; set; }
        public CityDto City { get; set; }
    }
}
