using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AKCodeHarness.CodeTests.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        #region "Setup"
        private Mock<IArchivedDataService> _archivedDataService;
        private Mock<ICustomerDataAccess> _customerDataAccess;
        private Mock<IFailoverService> _failoverService;
        private Mock<IFailoverCustomerDataAccessService> _failoverCustomerDataAccessService;
        private Mock<IAppSettingService> _appSettingService;
        private CustomerService _customerService;

        [TestInitialize]
        public void Setup()
        {
            _archivedDataService = new Mock<IArchivedDataService>();
            _customerDataAccess = new Mock<ICustomerDataAccess>();
            _failoverService = new Mock<IFailoverService>();
            _failoverCustomerDataAccessService = new Mock<IFailoverCustomerDataAccessService>();
            _appSettingService = new Mock<IAppSettingService>();

            _customerService = new CustomerService(_archivedDataService.Object, _customerDataAccess.Object
                                                    , _failoverService.Object, _failoverCustomerDataAccessService.Object);
        }
        #endregion

        #region "Test Methods"
        [TestMethod]
        public void GetCustomerFromArchived_Test()
        {
            //Arrange
            _archivedDataService.Setup(x => x.GetArchivedCustomer(45)).Returns(new Customer());

            //Act
            var Customer = _customerService.GetCustomer(45, true).Result;

            //Assert
            Assert.IsNotNull(Customer);
        }

        [TestMethod]
        public void GetCustomer_FailoverOff_MainDataStore_Test()
        {
            //Arrange
            _customerDataAccess.Setup(x => x.LoadCustomerAsync(45)).ReturnsAsync(GetCustomerResponseWithRealDataStore());

            //Act
            var Customer = _customerService.GetCustomer(45, false).Result;

            //Assert
            Assert.IsNotNull(Customer);
            Assert.AreEqual(Customer.Id, 45);
            Assert.AreEqual(Customer.Name, "abdul");
        }

        [TestMethod]
        public void GetCustomer_FailoverOff_ArchivedDataStore_Test()
        {
            //Arrange
            _customerDataAccess.Setup(x => x.LoadCustomerAsync(45)).ReturnsAsync(GetCustomerResponseWithArchived);
            _archivedDataService.Setup(x => x.GetArchivedCustomer(45)).Returns(new Customer { Id = 45, Name = "abdul" });

            //Act
            var Customer = _customerService.GetCustomer(45, false).Result;

            //Assert
            Assert.IsNotNull(Customer);
            Assert.AreEqual(Customer.Id, 45);
            Assert.AreEqual(Customer.Name, "abdul");
        }

        [TestMethod]
        public void GetCustomer_FailoverOn_FailoverMainDataStore_Test()
        {
            //Arrange
            _failoverService.Setup(x => x.OnFailover()).Returns(true);
            _failoverCustomerDataAccessService.Setup(x => x.GetCustomerById(45)).ReturnsAsync(GetCustomerResponseWithRealDataStore());

            //Act
            var Customer = _customerService.GetCustomer(45, false).Result;

            //Assert
            Assert.IsNotNull(Customer);
            Assert.AreEqual(Customer.Id, 45);
            Assert.AreEqual(Customer.Name, "abdul");
        }

        [TestMethod]
        public void GetCustomer_FailoverOn_FailoverArchivedDataStore_Test()
        {
            //Arrange
            _failoverService.Setup(x => x.OnFailover()).Returns(true);
            _failoverCustomerDataAccessService.Setup(x => x.GetCustomerById(45)).ReturnsAsync(GetCustomerResponseWithArchived());
            _archivedDataService.Setup(x => x.GetArchivedCustomer(45)).Returns(new Customer { Id = 45, Name = "abdul" });

            //Act
            var Customer = _customerService.GetCustomer(45, false).Result;

            //Assert
            Assert.IsNotNull(Customer);
            Assert.AreEqual(Customer.Id, 45);
            Assert.AreEqual(Customer.Name, "abdul");
        }
        #endregion

        #region "Mock Data"
        private CustomerResponse GetCustomerResponseWithRealDataStore()
        {
            new Customer { Id = 45, Name = "abdul" };
            return new CustomerResponse()
            {
                IsArchived = false,
                Customer = new Customer { Id = 45, Name = "abdul" }
            };
        }

        private CustomerResponse GetCustomerResponseWithArchived()
        {
            new Customer { Id = 45, Name = "abdul" };
            return new CustomerResponse()
            {
                IsArchived = true,
                Customer = new Customer { Id = 45, Name = "abdul" }
            };
        }
        #endregion

    }
}
