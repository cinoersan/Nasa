using Microsoft.Extensions.DependencyInjection;
using Nasa.Business.Repositories.Configuration;
using Nasa.Business.Repositories.Movements;
using Nasa.Business.Services.Commands;
using Nasa.Business.Services.FileHandlers;

namespace Nasa.Business.Config
{
    public static class BusinessConfig
    {

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IFileHandlerService, FileHandlerService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
        }
    }

}
