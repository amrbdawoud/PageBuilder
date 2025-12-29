using Microsoft.Extensions.DependencyInjection;
using PageBuilder.Domain.Interfaces;
using PageBuilder.Infrastructure.Repositories;

namespace PageBuilder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IPageRepository, PageRepository>();
        services.AddScoped<IBlockRepository, BlockRepository>();

        return services;
    }
}
