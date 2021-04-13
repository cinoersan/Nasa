using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Business.Profiles;

namespace Nasa.Business.Config
{
    public static class MapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LocationProfile());
            });
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
