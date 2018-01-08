﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PatientPortal.API.Provider.Models
{
    public static class URLBuilder
    {
        /// <summary>
        /// Get configuration URL Services
        /// </summary>
        /// <param name="apiSettingName"></param>
        /// <returns></returns>
        public static Uri APIBaseAddress(string apiSettingName)
        {
            return new Uri(ConfigurationManager.AppSettings[apiSettingName]);
        }

        /// <summary>
        /// Bind multiple parameter
        /// </summary>
        /// <param name="obj">Model parameter</param>
        /// <returns>String Uri</returns>
        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }


    
    }
}