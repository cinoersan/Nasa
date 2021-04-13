using Microsoft.EntityFrameworkCore;
using Nasa.Business.Data.Seedings;

namespace Nasa.Business.Data
{
    public partial class NasaDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedConfig();
        }
    }
}
