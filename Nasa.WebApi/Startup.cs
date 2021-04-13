using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nasa.Business.Config;
using Nasa.Business.Data;
using Nasa.WebApi.Config;
using Nasa.WebApi.Middlewares;

namespace Nasa.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddApiVersioning();
            services.AddCors(service => {
                service.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader()
                    .AllowAnyMethod());
            });
            services.AddDbContext<NasaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Nasa")));
            services.ConfigureBusinessServices();
            services.ConfigureSiteServices();
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapperConfig();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST", "OPTIONS", "GET"));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CustomExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
