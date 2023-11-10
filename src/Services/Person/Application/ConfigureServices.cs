using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ConfigureServices));
            services.AddAutoMapper(typeof(ConfigureServices));
        }
    }
}
