using System;
using System.Configuration;

namespace AKCodeHarness.CodeTests
{
    public class FailoverService : IFailoverService
    {
        private readonly IFailoverRepository _failoverRepository;
        private readonly IAppSettingService _appSettingService;

        public FailoverService(IFailoverRepository failoverRepository, IAppSettingService appSettingService)
        {
            this._failoverRepository = failoverRepository;
            this._appSettingService = appSettingService;
        }

        public bool OnFailover()
        {
            var failoverEntries = this._failoverRepository.GetFailOverEntries();


            var failedRequests = 0;

            foreach (var failoverEntry in failoverEntries)
            {
                if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                {
                    failedRequests++;
                }
            }

            return failedRequests > 100 && this._appSettingService.FailoverModeSetting();
        }

    }
}
