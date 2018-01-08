﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
// <copyright file="MemoryCacheObject.cs" company="FIS" department="R&D">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>LinhNT76</author>
// <date></date>
// <summary>Memory Cache</summary>
namespace PatientPortal.Domain.Caching.MemCache
{
    public class MemoryCacheObject
    {
        private static ObjectCache cache = MemoryCache.Default;
        public static void CacheObject(string name, object data, double minute = 10080) //7 Days
        {
            var policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.Default;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minute);

            cache.Set(name, data, policy);
        }

        public static object GetCacheObject(string name)
        {
            return cache[name] as object;
        }

        public static void RemoveCacheObject(string name)
        {
            if (cache.Contains(name))
            {
                cache.Remove(name);
            }
        }
    }

    public class ObjectCacheProfile
    {
        public const string CACHE_PROFILE_USER = "AF:";
        public const string CACHE_MODULE_USER = "ModuleUser";
        public const string CACHE_SPA_CONFIG = "SPAConfiguration";
    }
}
