namespace Market.Domain.Core;
public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Id { get; }

    protected TypedIdValueBase(Guid id)
    {
        if (id == Guid.Empty) {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) {
            return false;
        }

        return obj is TypedIdValueBase other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(TypedIdValueBase other)
    {
        return Id == other?.Id;
    }

    public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
    {
        if (object.Equals(obj1, null)) {
            if (object.Equals(obj2, null)) {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y)
    {
        return !(x == y);
    }
}
