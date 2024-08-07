using Customers.Application.Interfaces.V1.Repositories;
using Customers.Persistence.Repositories.V1;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Persistence
{
    public static class Installer
    {
        public static void InstallPersistenceLayer(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
