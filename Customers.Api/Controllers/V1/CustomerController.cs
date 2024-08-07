using AutoMapper;
using Customers.Api.Models.V1.Request;
using Customers.Api.Models.V1.Response;
using Customers.Application.Interfaces.V1.Services;
using Customers.Application.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customers.Api.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> Get(CancellationToken cancellationToken)
        {
            var customerData = await _customerService.GetAllAsync(cancellationToken);
            if(customerData == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<CustomerResponse>>(customerData));
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(id, cancellationToken);
            if (customer == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<CustomerResponse>(customer));
        }

        // POST api/<CustomerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] AddCustomerRequest addCustomerRequest, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCustomer = _mapper.Map<AddCustomerDataModel>(addCustomerRequest);
            await _customerService.AddAsync(newCustomer, cancellationToken);
            return Created();
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] UpdateCutomerRequest updateCustomerRequest, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCustomer = _mapper.Map<UpdateCustomerDataModel>(updateCustomerRequest);
            await _customerService.UpdateAsync(newCustomer, cancellationToken);
            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _customerService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
