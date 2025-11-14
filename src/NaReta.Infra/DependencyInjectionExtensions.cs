using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NaReta.Infra.DataAccess;

namespace NaReta.Infra;
public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext(services);
    }


    private static void AddDbContext(IServiceCollection services)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        string DbPath = Path.Join(path, "NaReta.db");

        services.AddDbContext<NaRetaDBContext>(config => config.UseSqlite($"Data Source={DbPath}"));
    }
}
