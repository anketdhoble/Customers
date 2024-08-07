using AutoMapper;
using Customers.Application.Models.V1;
using Customers.Domain.Entities;

namespace Customers.Persistence.Mapper
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCustomerDataModel, Customer>();
            CreateMap<UpdateCustomerDataModel, Customer>();
            CreateMap<Customer, CustomerDataModel>();
        }
    }
}
