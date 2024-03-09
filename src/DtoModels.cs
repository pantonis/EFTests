namespace EFTests
{
    public class ClientOrdersDto
    {
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string LatestOrderDetails { get; set; }
        public List<ProductDto> MostExpensiveProduct { get; set; }
        public DateTime LatestOrderTimestamp { get; set; }
        public List<AddressDto> DeliveryAddresses { get; set; }
    }

    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AddressType { get; set; }
    }
}
