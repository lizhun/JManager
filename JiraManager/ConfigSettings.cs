using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraManager
{
    public static class ConfigSettings
    {
        public static string JIRAUserName => ConfigurationManager.AppSettings["JIRAUserName"];
        public static string JIRAPassword => ConfigurationManager.AppSettings["JIRAPassword"];

        public static string JIRAServerUrl => ConfigurationManager.AppSettings["JIRAServerUrl"];
        public static string DBName => ConfigurationManager.AppSettings["DBName"];
    }
}
