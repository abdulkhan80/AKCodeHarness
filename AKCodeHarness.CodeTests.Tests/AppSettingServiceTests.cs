using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace AKCodeHarness.CodeTests.Tests
{
    [TestClass]
    public class AppSettingServiceTests
    {
        private AppSettingService _appSettingService;

        [TestInitialize]
        public void Setup()
        {            
            _appSettingService = new AppSettingService();
        }

        [TestMethod]
        public void FailoverMode_IsOFF_InSettings_Test()
        {
            Assert.IsFalse(_appSettingService.FailoverModeSetting());
        }

    }
}
