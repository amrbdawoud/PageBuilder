namespace PageBuilder.Application;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PageBuilder.Application.Interfaces;
using PageBuilder.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IBlockService, BlockService>();

        services.AddValidatorsFromAssemblyContaining<CompanyService>();

        return services;
    }
}
