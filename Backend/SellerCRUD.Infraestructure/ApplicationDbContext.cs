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
        }
    }
}
