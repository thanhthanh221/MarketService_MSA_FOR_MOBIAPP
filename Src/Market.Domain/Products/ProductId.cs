using Market.Domain.Core;

namespace Market.Domain.Products;
public class ProductId
{
    public Guid Id { get; }
    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Id = id;
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is ProductId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(ProductId other)
    {
        return Id == other?.Id;
    }

    public static bool operator ==(ProductId obj1, ProductId obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(ProductId x, ProductId y)
    {
        return !(x == y);
    }
}
