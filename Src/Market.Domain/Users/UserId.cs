using Market.Domain.Core;

namespace Market.Domain.Users;
public class UserId : TypedIdValueBase
{
    public UserId(Guid value) : base(value)
    {
    }
}
