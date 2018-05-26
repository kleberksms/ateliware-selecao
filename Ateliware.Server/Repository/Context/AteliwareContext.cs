using Microsoft.EntityFrameworkCore;

namespace Ateliware.Server.Repository.Context
{
    public class AteliwareContext : DbContext
    {
        public AteliwareContext(DbContextOptions<AteliwareContext> options) : base(options) { }

        public DbSet<Entities.Repository> Repositories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entities.Repository>().HasKey(m => m.Guid);
            base.OnModelCreating(builder);
        }
    }
}
