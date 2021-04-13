using Microsoft.EntityFrameworkCore;
using Nasa.Business.Models;

namespace Nasa.Business.Data
{
    public partial class NasaDbContext : DbContext
    {
        public NasaDbContext(DbContextOptions<NasaDbContext> options) : base(options)
        {

        }
        public DbSet<NasaConfig> NasaConfigs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
