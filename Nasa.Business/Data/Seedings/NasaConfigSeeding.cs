using Microsoft.EntityFrameworkCore;
using Nasa.Business.Models;
using Nasa.Model.Keys;

namespace Nasa.Business.Data.Seedings
{
    public static class NasaConfigSeeding
    {
        public static void SeedConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NasaConfig>()
                .HasData(
                    new NasaConfig {Id = 1, GroupCode = ConfigKeys.Plateau, Value = "{\"X\":5,\"Y\":5}" });
        }
    }

}
