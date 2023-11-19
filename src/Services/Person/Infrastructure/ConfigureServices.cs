using Application.Abstract;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Concrete;
using Persistence.Contexts;

namespace Persistence
{
    public static class ConfigureServices
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonService, PersonManager>();
            services.AddScoped<IReportService, ReportManager>();

            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            //using var dbcontext = new ApplicationDbContext(optionsBuilder.Options);
            //dbcontext.Database.EnsureCreated();
            //dbcontext.Database.Migrate();
        }
    }
}
