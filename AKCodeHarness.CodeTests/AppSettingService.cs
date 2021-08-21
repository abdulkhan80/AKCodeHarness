using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    /*  Abdul Khan: 31/07/18 14:19

    1) Use this class to get or set config settings .
    */

    public class AppSettingService : IAppSettingService
    {
        public bool FailoverModeSetting()
        {
            return ConfigService.IsFailoverModeEnabled;
        }
    }
}
