using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client
{
    public static class ConfigurationHelper
    {
        public static string GetConfig(string key) => System.Environment.GetEnvironmentVariable(key);
    }
}
