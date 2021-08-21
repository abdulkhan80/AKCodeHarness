using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace AKCodeHarness.CodeTests.Tests
{
    [TestClass]
    public class FailoverServiceTests
    {
        #region "Setup"
        private Mock<IFailoverRepository> _failoverRepository;
        private Mock<IAppSettingService> _appSettingService;
        private FailoverService _failoverService;

        [TestInitialize]
        public void Setup()
        {
            _failoverRepository = new Mock<IFailoverRepository>();
            _appSettingService = new Mock<IAppSettingService>();

            _failoverService = new FailoverService(_failoverRepository.Object,_appSettingService.Object);
        }
        #endregion

        #region "Test Methods"
        [TestMethod]
        public void On_Failover_IfModeisOFF_Test()
        {
            //Arrange
            _appSettingService.Setup(x => x.FailoverModeSetting()).Returns(false);
            _failoverRepository.Setup(x => x.GetFailOverEntries()).Returns(GetFailOverMockEntries(101));

            //Act
            bool failover = _failoverService.OnFailover();

            //Assert
            Assert.IsFalse(failover);
        }

        [TestMethod]
        public void On_Failover_IfModeisON_Test()
        {
            //Arrange
            _appSettingService.Setup(x => x.FailoverModeSetting()).Returns(true);
            _failoverRepository.Setup(x => x.GetFailOverEntries()).Returns(GetFailOverMockEntries(101));

            //Act
            bool failover = _failoverService.OnFailover();

            //Assert
            Assert.IsTrue(failover);
        }

        [TestMethod]
        public void Failover_IfModeisOn_AndNotGetMoreThan100Errors_Test()
        {
            //Arrange
            _appSettingService.Setup(x => x.FailoverModeSetting()).Returns(true);
            _failoverRepository.Setup(x => x.GetFailOverEntries()).Returns(GetFailOverMockEntries(99));

            //Act
            bool failover = _failoverService.OnFailover();

            //Assert
            Assert.IsFalse(failover);
        }
        #endregion

        #region "Mock Data"
        private List<FailoverEntry> GetFailOverMockEntries(int numberOfEntries)
        {
            var failedOverEntries = new List<FailoverEntry>();
            for (int counter = 0; counter <= numberOfEntries; counter++)
                failedOverEntries.Add(new FailoverEntry() { DateTime = DateTime.Now });
            return failedOverEntries;
        }
        #endregion

    }
}
