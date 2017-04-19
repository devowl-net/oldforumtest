using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace SimpleForum.App_Start
{
    public class SecurityConfig
    {
        public static void ConfigureSecurity()
        {
            WebSecurity.InitializeDatabaseConnection(@"DefaultConnection", 
                "Users", 
                "Id", "Email", true);
        }
    }
}