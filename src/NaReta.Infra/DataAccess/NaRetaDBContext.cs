using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NaReta.Domain.Entities;
using NaReta.Infra.Configuration;

namespace NaReta.Infra.DataAccess;
internal class NaRetaDBContext: DbContext
{
    private readonly DatabaseSettings _settings;
    public DbSet<Category> categories { get; set; }
    public DbSet<Transaction> transactions { get; set; }
    public DbSet<Account> accounts { get; set; }


    public NaRetaDBContext(DbContextOptions options, IOptions<DatabaseSettings> optionsSettings) : base(options)
    {
        _settings = optionsSettings.Value;

        if(string.IsNullOrWhiteSpace(_settings.DatabaseConnection))
            throw new InvalidOperationException("Database connection string is not configured");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(_settings.DatabaseConnection);
}
