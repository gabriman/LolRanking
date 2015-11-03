using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FirstSPA.Helpers
{
    public static class Config
    {
        public enum ConfigKeys
        {
            ImagesUrl, ApiKey
        }

        public static string Read(string property)
        {
            try
            {
                return ConfigurationManager.AppSettings["property"];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Read(ConfigKeys property)
        {
            try
            {
                return ConfigurationManager.AppSettings[property.ToString()];
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}