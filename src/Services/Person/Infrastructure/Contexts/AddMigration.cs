using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public static class AddMigration
    {
        public static void InitialMigration(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
    }
}
