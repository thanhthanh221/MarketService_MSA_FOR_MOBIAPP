using Market.Domain.Users;
using Market.Infrastructure.MarketContext;
using MongoDB.Driver;

namespace Market.Infrastructure.Domain.Users;
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserAggregate> userCollection;
    private readonly FilterDefinitionBuilder<UserAggregate> filterBuilder = Builders<UserAggregate>.Filter;

    public UserRepository(MarketDbContext context)
    {
        userCollection = context.Users;
    }
    public async Task CreateUserAsync(UserAggregate user)
    {
        await userCollection.InsertOneAsync(user);
    }

    public async Task<UserAggregate> GetUserByUserIdAsync(UserId userId)
    {
        var filter = filterBuilder?.Eq(p => p.UserId, userId);
        return await userCollection.Find(filter).FirstOrDefaultAsync();
    }
}
