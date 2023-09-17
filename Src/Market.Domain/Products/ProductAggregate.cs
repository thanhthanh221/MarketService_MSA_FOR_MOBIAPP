using Market.Domain.Core;
using Market.Domain.ProductComments.Exceptions;
using Market.Domain.Products.Events;
using Market.Domain.Products.Exceptions;
using Market.Domain.Users;

namespace Market.Domain.Products;

public class ProductAggregate : Entity, IAggregateRoot
{
    public ProductId ProductId { get; private set; }
    public ProductInfomation ProductInfomation { get; private set; }
    public ProductType ProductType { get; private set; }
    public ProductStatus ProductStatus { get; private set; }
    public List<ProductCategory> Categories { get; private set; }
    public ProductUser ProductUser { get; private set; }
    public ProductOrder ProductOrder { get; private set; }
    public ProductAggregate(
        ProductId productId,
        ProductInfomation productInfomation,
        ProductType productType,
        ProductStatus productStatus,
        List<ProductCategory> categories,
        ProductUser productUser,
        ProductOrder productOrder)
    {
        // Check Rule Create

        // Entity Value Base
        ProductId = productId;
        ProductInfomation = productInfomation;
        ProductType = productType;
        ProductStatus = productStatus;
        Categories = categories;
        ProductUser = productUser;
        ProductOrder = productOrder;

        AddDomainEvent(new CreatedProductDomainEvent(this));
    }

    public void LikeProduct(UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId.Id));
        if (!checkUserFavouriteProduct)
        {
            AddDomainEvent(new LikedProductDomainEvent(ProductId, userId));
            ProductUser.UserFavouriteProduct.Add(userId.Id);
            return;
        }
        throw new UserFavouriteProduct();
    }
    public void UnLikeProduct(UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId.Id));
        if (checkUserFavouriteProduct)
        {
            AddDomainEvent(new UnLikedProductDomainEvent(ProductId, userId));
            ProductUser.UserFavouriteProduct.Remove(userId.Id);
            return;
        }
        throw new UserDonotFavouriteProduct();
    }

    public void EvaluateProduct(int star, UserId userId)
    {
        if (star > 5 || star < 0)
        {
            throw new StarValueNotValidate();
        }
        if (ProductStatus.Equals(ProductStatus.Remove))
        {
            throw new ProductHasBeenDeleted();
        }

        double valueNewStar = (ProductInfomation.Star * ProductUser.CountEvaluated + star) / (ProductUser.CountEvaluated + 1);
        double newStar = Math.Round(valueNewStar, 1);
        ProductInfomation.SetStarProduct(newStar);
        ProductUser.CountEvaluated++;

        AddDomainEvent(new EvaluatedProductDomainEvent(userId, ProductId, newStar));
    }

    public void RemoveProduct(ProductId productId)
    {
        if (ProductStatus == ProductStatus.Active)
        {
            AddDomainEvent(new RemovedProductDomainEvent(productId));
            ProductStatus = ProductStatus.Remove;
            return;
        }
        throw new ProductHasBeenDeleted();
    }

    public void BuyProduct(UserId userId, ProductTypeValueId valueTypeOrderId, int countOrder)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        ProductType.ProductTypeValues.ForEach(p =>
        {
            if (valueTypeOrderId.Equals(p.ProductTypeValueId))
            {
                p.SetQuantityProductType(p.QuantityType - countOrder);
                p.SetQuantityProductTypeSold(p.QuantityProductTypeSold + countOrder);

                ProductTypeBoughtEvent productTypeBoughtEvent =
                    new(valueTypeOrderId, p.PriceType, countOrder);

                AddDomainEvent(new BoughtProductDomainEvent(userId, ProductId, productTypeBoughtEvent));
                return;
            }
        });
    }

    public void BuyFailProductProduct(
        UserId userId, ProductTypeValueId productTypeValueId, int countOrder)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        ProductType.ProductTypeValues.ForEach(p =>
        {
            if (productTypeValueId.Equals(p.ProductTypeValueId))
            {
                p.SetQuantityProductType(p.QuantityType + countOrder);
                p.SetQuantityProductTypeSold(p.QuantityProductTypeSold - countOrder);

                ProductTypeBoughtEvent productTypeBoughtEvent =
                    new(productTypeValueId, p.PriceType, countOrder);

                AddDomainEvent(new RollBackBoughtProductDomainEvent(
                    ProductId, userId, productTypeBoughtEvent));

                return;
            }
        });

    }
    public void CreateProductType(UserId adminId, List<ProductTypeValue> newProductTypeValues)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        HashSet<ProductTypeValue> newProductTypesValueToHasSet
            = ProductType.ProductTypeValues.ToHashSet();

        newProductTypeValues.ForEach(p => newProductTypesValueToHasSet.Add(p));

        if (newProductTypesValueToHasSet.Count == ProductType.ProductTypeValues.Count)
            throw new AllAddedProductTypesAlreadyExist();

        ProductType.SetProductType(newProductTypesValueToHasSet.ToList());
        AddDomainEvent(new CreatedProductTypeDomainEvent(ProductId, adminId, newProductTypeValues));
    }
}
