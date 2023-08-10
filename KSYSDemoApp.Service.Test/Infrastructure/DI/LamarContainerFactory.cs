using Lamar;
using Microsoft.Extensions.Configuration;

namespace KUSYSDemoApp.Service.Test.DI
{
    public class LamarContainerFactory : IDisposable
    {
        public IContainer Container { get; private set; }

        public LamarContainerFactory()
        {
            Container = new Container(_ => Initialize(_));
        }

        private static void Initialize(ServiceRegistry registry)
        {
            var config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json").Build();

            var lamar = new LamarMainRegistry(config);
            registry.AddRange(lamar);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}