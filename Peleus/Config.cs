using System;
using System.Collections.Generic;
using System.Configuration;

namespace Peleus
{
    class Config
    {
        public static readonly string DB;

        static Config()
        {
            DB = GetValue("Database");
        }

        private static string GetValue(string key)
        {
            string value = null;
            try
            {
                value = ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return value;
        }
    }
}
