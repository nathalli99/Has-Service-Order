using AutoMapper;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Services.Customers;
using Moq;
using Microsoft.AspNetCore.Http;
using OsDsII.api.Dtos;
using OsDsII.api.Models;

namespace HasServiceOrder.Tests.Service
{
    public class CustomersServiceTests
    {
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly CustomersService _service;

        public CustomersServiceTests()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _service = new CustomersService(_mockCustomersRepository.Object, _mockMapper.Object, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async void Should_Return_A_List_Of_Customers()
        {
            // gera uma lista estática de customersDto
            List<Customer> customers = new List<Customer>()
            {
                new Customer { Id = 1, Email = "fakemail@mail.com", Name = "Lucas Careca", Phone = "992398763297", ServiceOrders = null }
            };
            List<CustomerDto> customersDto = new List<CustomerDto>()
            {
                new CustomerDto { Id = 1, Email = "fakemail@mail.com", Name = "Lucas Careca", Phone = "992398763297", ServiceOrders = null },
            };
            _mockCustomersRepository.Setup(repository => repository.GetAllAsync()).ReturnsAsync(customers);
            _mockMapper.Setup(m => m.Map<IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>())).Returns(customersDto);
            var result = await _service.GetAllAsync();
            Assert.Equal(customersDto, result);
        }
    }
}
