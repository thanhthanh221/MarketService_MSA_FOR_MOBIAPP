namespace Market.Domain.Users;

public class UserAggregate
{
    public UserId UserId { get; private set; }
    public string UserName { get; private set; }
    public string AvarUri { get; private set; }
    
    public UserAggregate(UserId userId, string userName, string avarUri)
    {
        UserId = userId;
        UserName = userName;
        AvarUri = avarUri;
    }

    public UserAggregate()
    {
    }
}
