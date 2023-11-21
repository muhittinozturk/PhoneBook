using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Person.Test.Functional.Base
{
    public class BaseHost : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {

            builder.ConfigureAppConfiguration(c =>
            {
                var directory = Path.GetDirectoryName(typeof(BaseHost).Assembly.Location)!;

                c.AddJsonFile(Path.Combine(directory, "appsettings.person.json"), optional: false);

            });

            return base.CreateHost(builder);
        }
    }
}