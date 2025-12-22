using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;
using NaReta.Infra.DataAccess;
using NaReta.Infra.DataAccess.Repositories;

namespace NaReta.Infra;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRepository(services);
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();
        services.AddScoped<ICategoryWriteOnlyRepository, CategoryRepository>();
        services.AddScoped<ITransactionReadOnlyRepository, TransactionRepository>();
        services.AddScoped<ITransactionWriteOnlyRepository, TransactionRepository>();
        services.AddScoped<IAccountReadOnlyRepository, AccountRepository>();
        services.AddScoped<IAccountWriteOnlyRepository, AccountRepository>();
    }

    private static void AddDbContext(IServiceCollection services)
    {
        services.AddDbContext<NaRetaDBContext>();
    }
}
