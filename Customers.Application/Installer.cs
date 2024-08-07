using Customers.Application.Interfaces.V1.Services;
using Customers.Application.Services.V1;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Application
{
    public static class Installer
    {
        public static void InstallApplicationLayer(this IServiceCollection service)
        {
            service.AddScoped<ICustomerService,CustomerService>();
        }
    }
}
