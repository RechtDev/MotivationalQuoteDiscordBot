using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectElectra.Querys;

namespace ProjectElectra
{
    internal class Startup
    {
        public static ServiceProvider ConfigureServices(IServiceCollection services, Microsoft.Extensions.Configuration.IConfigurationRoot config)
        {
            services.AddDbContext<SettingsDbContext>(options => options.UseSqlServer(config.GetConnectionString("ServerSettingsSQL")));
            services.AddScoped<IQuerySettingsDb, QuerySettingsDbSQL>();
            return services.BuildServiceProvider();
        }
    }
}