namespace SellerCRUD.Services.DTOS
{
    public class CreateSellerDto
    {
        public CreateSellerDto() { }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int IdentificationNumber { get; set; }
        public int? CityId { get; set; }
    }
}
