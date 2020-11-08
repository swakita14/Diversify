using Diversify_Server.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Data
{
    public class DiversifyContext : DbContext
    {
        public DiversifyContext(DbContextOptions<DiversifyContext> options)
            : base(options)
        {
        }

        public DbSet<Industry> Industry { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Stock> Stock { get; set; }

        /**
         * Model Builder
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IndustryConfiguration());
            
            modelBuilder.ApplyConfiguration(new SectorConfiguration());
            
            modelBuilder.ApplyConfiguration(new StockConfiguration());
        }
    }
}
