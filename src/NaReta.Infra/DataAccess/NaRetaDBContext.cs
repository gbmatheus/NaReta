using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;

namespace NaReta.Infra.DataAccess;
internal class NaRetaDBContext: DbContext
{
    public DbSet<Category> categories { get; set; }
    public DbSet<Transaction> transactions { get; set; }
    public DbSet<Account> accounts { get; set; }


    public NaRetaDBContext(DbContextOptions options) : base(options)
    {
    }
}
