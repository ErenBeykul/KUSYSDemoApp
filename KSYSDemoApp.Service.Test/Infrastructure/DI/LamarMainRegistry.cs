using Lamar;
using KUSYSDemoApp.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace KUSYSDemoApp.Service.Test.DI
{
    public class LamarMainRegistry : ServiceRegistry
    {
        public LamarMainRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.Assembly("KUSYSDemoApp.Service");
                x.Assembly("KUSYSDemoApp.Service.Test");
                x.Assembly("KUSYSDemoApp.DataAccess");
            });

            string connectionString = configuration.GetConnectionString("KUSYSDemoAppContext")!;
            DbContextOptionsBuilder<KUSYSDemoAppContext> optionsBuilder = new();
            optionsBuilder.UseNpgsql(connectionString);

            For<IKUSYSDemoAppContext>().Use<KUSYSDemoAppContext>()
                  .Ctor<DbContextOptions<KUSYSDemoAppContext>>("options")
                              .Is(optionsBuilder.Options);

            For<IConfiguration>().Use(configuration);
        }
    }
}