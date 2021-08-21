using System;
using System.Configuration;

namespace AKCodeHarness.CodeTests
{
    /*  Abdul Khan: 31/07/18 14:15

    1) Good to maintain all configs getter/setter settings in one static class.
    */

    public static class ConfigService
    {
        public static bool IsFailoverModeEnabled
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsFailoverModeEnabled"]);
            }
        }
    }
}
