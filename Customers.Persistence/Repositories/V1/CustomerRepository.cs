using AutoMapper;
using Customers.Application.Interfaces.V1.Repositories;
using Customers.Application.Models.V1;
using Customers.Domain.Entities;
using Customers.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence.Repositories.V1
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(AddCustomerDataModel customer, CancellationToken cancellationToken)
        {
            var newCustomer = _mapper.Map<Customer>(customer);
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(id, cancellationToken);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerDataModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customerList = await _context.Customers.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CustomerDataModel>>(customerList);
        }

        public async Task<CustomerDataModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(id, cancellationToken);
            return _mapper.Map<CustomerDataModel>(customer);
        }

        public async Task UpdateAsync(UpdateCustomerDataModel customer, CancellationToken cancellationToken)
        {
            var updateCustomer = _mapper.Map<Customer>(customer);
            _context.Customers.Update(updateCustomer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
