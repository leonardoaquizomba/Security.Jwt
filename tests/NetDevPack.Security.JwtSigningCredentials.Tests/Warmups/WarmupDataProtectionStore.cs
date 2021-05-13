using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NetDevPack.Security.JwtSigningCredentials.Tests.Warmups
{
    /// <summary>
    /// 
    /// </summary>
    public class WarmupDataProtectionStore : IWarmupTest
    {
        public WarmupDataProtectionStore()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddJwksManager().PersistKeysToDataProtection();

            Services = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider Services { get; set; }

        public void DetachAll()
        {

            var database = Services.GetService<AspNetGeneralContext>();
            foreach (var dbEntityEntry in database.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }

        }
    }
}