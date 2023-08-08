using Market.Domain.Coupons;
using Market.Domain.ProductComments;
using Market.Domain.Products;
using Market.Domain.Users;
using Market.Infrastructure.Domain.Coupons;
using Market.Infrastructure.Domain.ProductComments;
using Market.Infrastructure.Domain.Products;
using Market.Infrastructure.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public class RepositoryDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
