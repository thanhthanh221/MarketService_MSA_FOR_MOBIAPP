using Market.Domain.Core;
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

        this.AddDomainEvent(new ProductCreatedDomainEvent(this));
    }

    public void UserFavouriteProduct(ProductId productId, UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove)) 
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId));
        if (!checkUserFavouriteProduct) {
            this.AddDomainEvent(new ProductUserFavouriteDomainEvent(productId, userId));
            ProductUser.UserFavouriteProduct.Add(userId);
            return;
        }
        throw new UserFavouriteProduct();
    }

    public void UserRemovedFavourite(ProductId productId, UserId userId)
    {
        if (ProductStatus.Equals(ProductStatus.Remove)) 
            throw new ProductHasBeenDeleted();

        bool checkUserFavouriteProduct = ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId));
        if (checkUserFavouriteProduct) {
            this.AddDomainEvent(new ProductUserRemovedFavouriteDomainEvent(productId, userId));
            ProductUser.UserFavouriteProduct.Remove(userId);
            return;
        }
        throw new UserDonotFavouriteProduct();
    }

    public void ProductRemoved(ProductId productId)
    {
        if (ProductStatus == ProductStatus.Active) {
            this.AddDomainEvent(new ProductRemovedDomainEvent(productId));
            ProductStatus = ProductStatus.Remove;
            return;
        }
        throw new ProductHasBeenDeleted();
    }

    public void UserOrderProductSuccess(
        UserId userId, List<ProductTypeUserOrderEvent> ProductTypeUserOrders)
    {
        if (ProductStatus.Equals(ProductStatus.Remove)) 
            throw new ProductHasBeenDeleted();

        ProductType.ProductTypeValues.ForEach(p => {
            var productTypeUserOrder = ProductTypeUserOrders.SingleOrDefault(pOrder =>
                pOrder.ValueTypeOrderId.TypeId == p.ProductTypeValueId.TypeId);
            if (productTypeUserOrder != null) {
                p.SetQuantityProductType(p.QuantityType - productTypeUserOrder.CountOrder);
                p.SetQuantityProductTypeSold(p.QuantityProductTypeSold + productTypeUserOrder.CountOrder);
            }
        });

        this.AddDomainEvent(new ProductUserOrderedProductSuccessDomainEvent(
            ProductId, userId, ProductTypeUserOrders));
    }

    public void CreatedNewProductType(UserId adminId, List<ProductTypeValue> newProductTypeValues)
    {
        if (ProductStatus.Equals(ProductStatus.Remove)) 
            throw new ProductHasBeenDeleted();

        HashSet<ProductTypeValue> newProductTypesValueToHasSet 
            = ProductType.ProductTypeValues.ToHashSet();
            
        newProductTypeValues.ForEach(p => newProductTypesValueToHasSet.Add(p));

        if(newProductTypesValueToHasSet.Count == ProductType.ProductTypeValues.Count) 
            throw new AllAddedProductTypesAlreadyExist();

        ProductType.SetProductType(newProductTypesValueToHasSet.ToList());
        this.AddDomainEvent(new ProductCreatedNewProductTypeDomainEvent(ProductId, adminId, newProductTypeValues));

    }
}
