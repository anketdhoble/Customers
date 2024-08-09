using AutoFixture;
using AutoMapper;
using Customers.Api.Controllers.V1;
using Customers.Api.Mapper;
using Customers.Api.Models.V1.Request;
using Customers.Api.Models.V1.Response;
using Customers.Application.Interfaces.V1.Services;
using Customers.Application.Models.V1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Api.Tests.Controllers.V1
{
    public class CustomerControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICustomerService> _serviceMock;
        private readonly CustomerController _sut;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly CancellationToken _cancellationToken;

        public CustomerControllerTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<ICustomerService>>();
            _sut = new CustomerController(_serviceMock.Object, _mapper);//creates the implementation in-memory
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public void CustomerControllerConstructor_ShouldReturnNullReferenceException_WhenServiceIsNull()
        {
            // Arrange
            ICustomerService? customerService = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CustomerController(customerService,_mapper));
        }

        [Fact]
        public async Task GetCustomers_ShouldReturnOkResponse_WhenDataFound()
        {

            // Arrange
            var customerMock = _fixture.Create<IEnumerable<CustomerDataModel>>();
            _serviceMock.Setup(x => x.GetAllAsync(_cancellationToken)).ReturnsAsync(customerMock);

            // Act
            var result = await _sut.Get(_cancellationToken);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<CustomerResponse>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _serviceMock.Verify(x => x.GetAllAsync(_cancellationToken), Times.Once());
        }
        [Fact]
        public async Task GetCustomers_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            List<CustomerDataModel>? response = null;
            _serviceMock.Setup(x => x.GetAllAsync(_cancellationToken)).ReturnsAsync(response);

            // Act
            var result = await _sut.Get(_cancellationToken);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.GetAllAsync(_cancellationToken), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnOkResponse_WhenValidInput()
        {
            // Arrange
            var customerResponseMock = _fixture.Create<CustomerResponse>();
            var customerMock = _fixture.Create<CustomerDataModel>();
            var id = _fixture.Create<Guid>();
            _serviceMock.Setup(x => x.GetByIdAsync(id,_cancellationToken)).ReturnsAsync(customerMock);

            // Act
            var result = await _sut.GetById(id,_cancellationToken);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<CustomerResponse>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(customerResponseMock.GetType());
            _serviceMock.Verify(x => x.GetByIdAsync(id,_cancellationToken), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnNotFound_WhenNoDataFound()
        {
            // Arrange
            CustomerDataModel? response = null;
            var id = _fixture.Create<Guid>();
            _serviceMock.Setup(x => x.GetByIdAsync(id,_cancellationToken)).ReturnsAsync(response);

            // Act
            var result = await _sut.GetById(id,_cancellationToken);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.GetByIdAsync(id, _cancellationToken), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnBadRequest_WhenInputIsEmpty()
        {
            // Arrange
            var response = _fixture.Create<CustomerDataModel>();
            Guid id = Guid.Empty;

            // Act
            var result = await _sut.GetById(id,_cancellationToken);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _serviceMock.Setup(x => x.DeleteAsync(id,_cancellationToken)).ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(id,_cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();

        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNotFound_WhenRecordNotFound()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _serviceMock.Setup(x => x.DeleteAsync(id,_cancellationToken)).ReturnsAsync(false);

            // Act
            var result = await _sut.Delete(id,_cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
        }
    }
}
