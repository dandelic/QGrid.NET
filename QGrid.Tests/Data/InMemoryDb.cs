using Microsoft.EntityFrameworkCore;

namespace QGrid.Tests.Data
{
    public class InMemoryDb : DbContext
    {
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; } = null!;

        internal InMemoryDb(DbContextOptions<InMemoryDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InMemoryDb).Assembly);
        }
    }
}
