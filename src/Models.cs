using CSharpFunctionalExtensions;

namespace EFTests
{
    public class Client
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
        public long ClientId { get; set; }

        // Navigation properties
        public Client Client { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string AddressType { get; private set; }

        private Address()
        {
        }

        private Address(string street, string city, string country, string addressType)
        {
            Street = street;
            City = city;
            Country = country;
            AddressType = addressType;
        }

        private static Result<Address> Create(string street, string city, string country, string addressType)
        {
            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<Address>("Street should not be empty");

            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<Address>("City should not be empty");

            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<Address>("Country should not be empty");

            return Result.Success(new Address(street, city, country, addressType));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return AddressType;
        }
    }
}
