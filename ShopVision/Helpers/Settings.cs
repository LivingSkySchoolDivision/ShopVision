using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public static class Settings
    {
        public static string LoginURL { get { return "/Login/index.aspx"; } }
        public static string IndexURL { get { return "/index.aspx"; } }

        public static string ApplicationRoot { get { return HttpContext.Current.Request.ApplicationPath; } }
        public static string CookieName { get { return "LSKYSDSHOPVISION"; } }
        public static string Domain { get { return "LSKYSD.CA"; } }

        public static string DBConnectionString_FleetVision
        {
            get { return ConfigurationManager.ConnectionStrings["FleetVision"].ConnectionString; }
        }

        public static string DBConnectionString_VersaTrans
        {
            get { return ConfigurationManager.ConnectionStrings["Versatrans"].ConnectionString; }
        }

        public static string DBConnectionString_ShopVision
        {
            get { return ConfigurationManager.ConnectionStrings["ShopVision"].ConnectionString; }
        }
        public static string GetServerName(HttpRequest Request)
        {
            return Request.ServerVariables["SERVER_NAME"].ToString().Trim();
        }

        public static List<string> Groups_AllowedAccess
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["allowed_ad_security_groups"].ToString().Split(';').ToList().ConvertAll(g => g.ToLower());
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

        public static List<string> Groups_AdminAccess
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["admin_ad_security_groups"].ToString().Split(';').ToList().ConvertAll(g => g.ToLower());
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

    }
}