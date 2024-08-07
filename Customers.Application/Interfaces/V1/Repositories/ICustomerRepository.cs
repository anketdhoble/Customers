using Customers.Application.Models.V1;

namespace Customers.Application.Interfaces.V1.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDataModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CustomerDataModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(AddCustomerDataModel customer, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateCustomerDataModel customer, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
