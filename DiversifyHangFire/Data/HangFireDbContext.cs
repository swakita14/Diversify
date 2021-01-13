using DiversifyCL.Data;
using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DiversifyHangFire.Data
{
    public class HangFireDbContext : DbContext
    {
        public HangFireDbContext(DbContextOptions<HangFireDbContext> options) : base(options)
        {
            
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }
    }
}
