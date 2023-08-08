using Market.Domain.Coupons;
using Market.Domain.ProductComments;
using Market.Domain.Products;
using Market.Domain.Users;
using Market.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Market.Infrastructure.MarketContext;
public class MarketDbContext : MongoClient
{
    public MongoClient Client { get; set; }

    public IMongoCollection<ProductAggregate> Products { get; set; }
    public IMongoCollection<CouponAggregate> Coupons { get; set; }
    public IMongoCollection<ProductCommentAggregate> ProductComments { get; set; }
    public IMongoCollection<UserAggregate> Users { get; set; }

    public MarketDbContext(IOptions<MongoDbSettings> dbSettings)
    {
        Client = new MongoClient(dbSettings.Value.ConnectionString);
        var dataBase = Client.GetDatabase(dbSettings.Value.Name);

        // Config Data Table
        Products = dataBase.GetCollection<ProductAggregate>("Product");
        Coupons = dataBase.GetCollection<CouponAggregate>("Coupon");
        ProductComments = dataBase.GetCollection<ProductCommentAggregate>("ProductComment");
        Users = dataBase.GetCollection<UserAggregate>("User");
    }
}
