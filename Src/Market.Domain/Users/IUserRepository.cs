namespace Market.Domain.Users;
public interface IUserRepository
{
    Task CreateUserAsync(UserAggregate user);
    Task<UserAggregate> GetUserByUserIdAsync(UserId userId);
    
}
