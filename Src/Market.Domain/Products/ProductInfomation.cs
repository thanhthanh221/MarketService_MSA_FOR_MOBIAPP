namespace Market.Domain.Products;
public class ProductInfomation
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Calo { get; private set; }
    public string Descretion { get; private set; }
    public double Star { get; private set; }
    public string ProductImageUri { get; private set; }
    public DateTime CreateAt { get; private set; }

    public ProductInfomation(
        string name, decimal price, int calo, string descretion, double star, string productImageUri, DateTime createAt)
    {
        Name = name;
        Price = price;
        Calo = calo;
        Descretion = descretion;
        Star = star;
        ProductImageUri = productImageUri;
        CreateAt = createAt;
    }
    public ProductInfomation(
        string name, decimal price, int calo, string descretion, string productImageUri)
    {
        Name = name;
        Price = price;
        Calo = calo;
        Descretion = descretion;
        Star = 0;
        ProductImageUri = productImageUri;
        CreateAt = DateTime.UtcNow;
    }
    public void SetStarProduct(double newStar){
        Star = newStar;
    }
}
