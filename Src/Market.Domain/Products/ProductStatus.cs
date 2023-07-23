using Market.Domain.Core;

namespace Market.Domain.Products;

public class ProductStatus : Enumeration
{
    public static ProductStatus Active { get; private set; } = new(0, "Hoạt Động");
    public static ProductStatus Remove { get; private set; } = new(1, "Đã Xóa");

    public string StatusValue { get; private set; }
    public ProductStatus(int id, string statusValue)
    {
        (Id, StatusValue) = (id, statusValue);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType()) {
            return false;
        }

        ProductStatus otherObject = (ProductStatus)obj;
        
        return Id == otherObject.Id && StatusValue == otherObject.StatusValue;
    }

    public override int GetHashCode()
    {
        return $"{Id}+{StatusValue}".GetHashCode();
    }
}
