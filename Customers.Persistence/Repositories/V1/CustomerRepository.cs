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
        public async Task<CustomerDataModel> AddAsync(AddCustomerDataModel customer, CancellationToken cancellationToken)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var newCustomer = _mapper.Map<Customer>(customer);
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerDataModel>(newCustomer);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(id, cancellationToken);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<bool> UpdateAsync(UpdateCustomerDataModel customer, CancellationToken cancellationToken)
        {
            var customerToDelete = await _context.Customers.FindAsync(customer.Id);
            if(customerToDelete == null)
            {
                return false;
            }
            _context.Entry(customerToDelete).State = EntityState.Modified;
            customerToDelete.FirstName = customer.FirstName;
            customerToDelete.LastName = customer.LastName;
            customerToDelete.City = customer.City;
            customerToDelete.EmailId = customer.EmailId;
            customerToDelete.UpdatedDateTime = customer.UpdatedDateTime;
            _context.Customers.Update(customerToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
