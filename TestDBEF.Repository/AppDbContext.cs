using Microsoft.EntityFrameworkCore;
using TestDBEF.Repository.Entity;


namespace TestDBEF.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options){}
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Orders> Orders {  get; set; }
    }
}
