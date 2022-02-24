using Microsoft.EntityFrameworkCore;
using SellerCRUD.Domain.Entities;
using System.Reflection;

namespace SellerCRUD.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(mb);

            mb.HasSequence<int>("OrderNumbers", schema: "shared")
            .StartsAt(10)
            .IncrementsBy(10);
        }
    }
}
