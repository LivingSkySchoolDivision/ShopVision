using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public static class Settings
    {
        public static string DBConnectionString_FleetVision
        {
            get { return ConfigurationManager.ConnectionStrings["FleetVision"].ConnectionString; }
        }

        public static string DBConnectionString_VersaTrans
        {
            get { return ConfigurationManager.ConnectionStrings["Versatrans"].ConnectionString; }
        }
    }
}