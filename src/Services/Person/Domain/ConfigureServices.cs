using Domain.Entities;
using Domain.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Person>, PersonValidator>();
            services.AddValidatorsFromAssemblyContaining<PersonValidator>();
            return services;
        }
    }
}
