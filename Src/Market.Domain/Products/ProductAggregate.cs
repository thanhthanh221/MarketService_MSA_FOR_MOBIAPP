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

        AddDomainEvent(new ProductCreatedDomainEvent(this));
    }

    public void UserFavouriteProduct(UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId.Id));
        if (!checkUserFavouriteProduct)
        {
            AddDomainEvent(new ProductUserFavouriteDomainEvent(ProductId, userId));
            ProductUser.UserFavouriteProduct.Add(userId.Id);
            return;
        }
        throw new UserFavouriteProduct();
    }
    public void UserRemovedFavourite(ProductId productId, UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId.Id));
        if (checkUserFavouriteProduct)
        {
            AddDomainEvent(new ProductUserRemovedFavouriteDomainEvent(productId, userId));
            ProductUser.UserFavouriteProduct.Remove(userId.Id);
            return;
        }
        throw new UserDonotFavouriteProduct();
    }

    public void UserEvaluateProduct(int star, UserId userId)
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
        
        AddDomainEvent(new ProductEvaluatedByUserDomainEvent(userId, ProductId, newStar));
    }

    public void ProductRemoved(ProductId productId)
    {
        if (ProductStatus == ProductStatus.Active)
        {
            AddDomainEvent(new ProductRemovedDomainEvent(productId));
            ProductStatus = ProductStatus.Remove;
            return;
        }
        throw new ProductHasBeenDeleted();
    }

    public void UserOrderProductSuccess(
        UserId userId, ProductTypeValueId valueTypeOrderId, int countOrder)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        ProductType.ProductTypeValues.ForEach(p =>
        {
            if (valueTypeOrderId.Equals(p.ProductTypeValueId))
            {
                p.SetQuantityProductType(p.QuantityType - countOrder);
                p.SetQuantityProductTypeSold(p.QuantityProductTypeSold + countOrder);

                ProductTypeUserOrderEvent productTypeUserOrder =
                    new(valueTypeOrderId, p.ValueType, p.PriceType, countOrder);

                AddDomainEvent(new ProductUserOrderedProductSuccessDomainEvent(
                    ProductId, userId, productTypeUserOrder));

                return;
            }
        });
    }

    public void UserOrderRecoveredProduct(
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

                ProductTypeUserOrderEvent productTypeUserOrder =
                    new(productTypeValueId, p.ValueType, p.PriceType, countOrder);

                AddDomainEvent(new ProductUserOrderedProductRecoverdDomainEvent(
                    ProductId, userId, productTypeUserOrder));

                return;
            }
        });

    }
    public void CreatedNewProductType(UserId adminId, List<ProductTypeValue> newProductTypeValues)
    {
        if (ProductStatus.Equals(ProductStatus.Remove))
            throw new ProductHasBeenDeleted();

        HashSet<ProductTypeValue> newProductTypesValueToHasSet
            = ProductType.ProductTypeValues.ToHashSet();

        newProductTypeValues.ForEach(p => newProductTypesValueToHasSet.Add(p));

        if (newProductTypesValueToHasSet.Count == ProductType.ProductTypeValues.Count)
            throw new AllAddedProductTypesAlreadyExist();

        ProductType.SetProductType(newProductTypesValueToHasSet.ToList());
        AddDomainEvent(new
            ProductCreatedNewProductTypeDomainEvent(ProductId, adminId, newProductTypeValues));

    }
}
