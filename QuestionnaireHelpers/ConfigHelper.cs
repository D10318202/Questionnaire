using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QuestionHelpers
{
    public class ConfigHelper
    {
        public static string _MainDBName = "MainDB";

        //Function可以多載
        public static string GetConnectionString()
        {
            return GetConnectionString(_MainDBName);
        }

        public static string GetConnectionString(string name)
        {
            string connString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connString;
        }
    }
}