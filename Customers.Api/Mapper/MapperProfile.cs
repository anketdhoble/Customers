using AutoMapper;
using Customers.Api.Models.V1.Request;
using Customers.Api.Models.V1.Response;
using Customers.Application.Models.V1;

namespace Customers.Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCustomerRequest, AddCustomerDataModel>();
            CreateMap<UpdateCutomerRequest, UpdateCustomerDataModel>();
            CreateMap<CustomerDataModel, CustomerResponse>();
        }
    }
}
