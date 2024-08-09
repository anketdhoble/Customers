using Customers.Application.Models.V1;

namespace Customers.Application.Interfaces.V1.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDataModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CustomerDataModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<CustomerDataModel> AddAsync(AddCustomerDataModel customer, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(UpdateCustomerDataModel customer, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
