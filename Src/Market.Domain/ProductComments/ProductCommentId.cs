namespace Market.Domain.ProductComments;
public class ProductCommentId
{
    public Guid Id { get; }
    public ProductCommentId(Guid id)
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

        return obj is ProductCommentId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(ProductCommentId other)
    {
        return Id == other?.Id;
    }

    public static bool operator ==(ProductCommentId obj1, ProductCommentId obj2)
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

    public static bool operator !=(ProductCommentId x, ProductCommentId y)
    {
        return !(x == y);
    }
}

