using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace EFTests
{
    public class ClientService
    {
        public async Task<IEnumerable<ClientOrdersDto>> GetLatestOrderForEachClient(MyDbContext dbContext)
        {
            var clientOrders = await (from client in dbContext.Clients.AsNoTracking()
                                                                .Include(c => c.Orders).ThenInclude(o => o.Products)
                                      select new ClientOrdersDto
                                      {
                                          ClientId = client.Id,
                                          ClientName = client.Name,
                                          LatestOrderDetails = client.Orders.OrderByDescending(o => o.Timestamp).FirstOrDefault().Details,
                                          MostExpensiveProduct = client.Orders.OrderByDescending(o => o.Timestamp).FirstOrDefault().Products
                                                                    .Where(x => x.Name.StartsWith("A"))
                                                                    .Select(x => new ProductDto
                                                                    {
                                                                        Id = x.Id,
                                                                        Name = x.Name,
                                                                        Price = x.Price
                                                                    }).ToList(),
                                          DeliveryAddresses = client.Orders.OrderByDescending(o => o.Timestamp).FirstOrDefault().Addresses
                                              .Where(x => x.AddressType == "Delivery")
                                              .Select(x => new AddressDto
                                              {
                                                  Street = x.Street,
                                                  City = x.City,
                                                  Country = x.Country,
                                                  AddressType = x.AddressType
                                              }).ToList(),
                                      }).ToListAsync();

            return clientOrders;
        }
    }
}
