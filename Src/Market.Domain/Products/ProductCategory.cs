using Market.Domain.Core;

namespace Market.Domain.Products;

public class ProductCategory : Enumeration
{
    public ProductCategory(int id, string categoryName, string categoryIconUri)
    {
        (Id, CategoryName, CategoryIconUri) = (id, categoryName, categoryIconUri);
    }
    public string CategoryName { get; private set; }
    public string CategoryIconUri { get; private set; }

    // Category Value
    public static ProductCategory Bbq { get; private set; } = new(0, "bbq", "bbq.png");
    public static ProductCategory Bread { get; private set; } = new(1, "Bánh mỳ", "bread.png");
    public static ProductCategory Iceream { get; private set; } = new(2, "Bánh mỳ", "iceream.png");
    public static ProductCategory FishingTackle { get; private set; } = new(3, "Hải sản", "fishingTackle.png");
    public static ProductCategory Cherry { get; private set; } = new(4, "Hoa quả", "Cherry.png");
    public static ProductCategory Cafe { get; private set; } = new(5, "Cafe", "Cafe.png");
    public static ProductCategory Rice { get; private set; } = new(6, "Cơm", "Rice.png");
    public static ProductCategory FruitJuice { get; private set; } = new(7, "Nước ép hoa quả", "fruitJuice.png");
    public static ProductCategory Sugar { get; private set; } = new(8, "Đường", "sugar.png");
    public static ProductCategory Popcorn { get; private set; } = new(9, "Bắp rang", "popcorn.png");
    public static ProductCategory Pies { get; private set; } = new(10, "Bánh nướng", "pies.png");
    public static ProductCategory Jam { get; private set; } = new(11, "Mứt", "jam.png");

    public static IEnumerable<ProductCategory> GetAllCategoryProduct()
    {
        return new[]{
            Bbq, Bread, Iceream, FishingTackle, Cherry, Cafe,
            Rice, FruitJuice, Sugar, Popcorn, Pies, Jam
        };
    }
    public static ProductCategory GetCategoryById(int categoryId)
    {
        return GetAllCategoryProduct().SingleOrDefault(c => c.Id == categoryId);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType()) {
            return false;
        }

        ProductCategory otherObject = (ProductCategory)obj;
        
        return CategoryName == otherObject.CategoryName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Id, CategoryName, CategoryIconUri);
    }
}
