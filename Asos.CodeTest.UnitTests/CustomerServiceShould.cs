using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Asos.CodeTest.UnitTests
{
    [TestFixture]
    public sealed class CustomerServiceShould
    {        
        private CustomerService _customerService;

        private Mock<IFailoverRepository> _failoverRepository;

        private Mock<IAppSettings> _appSettings;

        private Mock<ICustomerDataAccess> _customerDataAccess;
            
        [SetUp]
        public void Init()
        {
            _failoverRepository = new Mock<IFailoverRepository>();
            _customerDataAccess = new Mock<ICustomerDataAccess>();
            _appSettings = new Mock<IAppSettings>();

            _customerService = new CustomerService(_appSettings.Object, _failoverRepository.Object,
                _customerDataAccess.Object);
        }

        [Test]
        public async Task ReturnsCustomerFromMainCustomerDataStore_WHEN_NotInFailOver_AND_NotArchived()
        {
            // Arrange
            const int customerId = 12345;
            var emptyFailoverEntries = new List<FailoverEntry>();
            var expectedCustomer = new Customer {Id = customerId, Name = "Test Customer"};
            _appSettings.Setup(mock => mock.IsFailoverModeEnabled).Returns(true);
            _failoverRepository.Setup(mock => mock.GetFailOverEntries()).Returns(emptyFailoverEntries);

            _customerDataAccess.Setup(mock => mock.LoadCustomerAsync(customerId))
                .ReturnsAsync(new CustomerResponse {Customer = expectedCustomer});

            // Act
            var result = await _customerService.GetCustomer(customerId, false);

            // Assert
            Assert.AreEqual(expectedCustomer, result);
        }
    }
}