using System.Reflection;

namespace Market.Domain.Core;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) {
            return false;
        }
        return ReferenceEquals(left, null) || left.Equals(right);
    }
}