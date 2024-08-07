using Customers.Application.Interfaces.V1.Repositories;
using Customers.Application.Interfaces.V1.Services;
using Customers.Application.Models.V1;

namespace Customers.Application.Services.V1
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddAsync(AddCustomerDataModel customer, CancellationToken cancellationToken)
        {
            await _customerRepository.AddAsync(customer, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<CustomerDataModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAllAsync(cancellationToken);
        }

        public async Task<CustomerDataModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(UpdateCustomerDataModel customer, CancellationToken cancellationToken)
        {
            await _customerRepository.UpdateAsync(customer, cancellationToken);
        }
    }
}
