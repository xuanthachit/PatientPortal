﻿using Newtonsoft.Json;
using PatientPortal.Domain.LogManager;
using PatientPortal.Provider.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApiContrib.Formatting;

// <copyright file="APIProvider.cs" company="FIS" department="R&D">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>LinhNT76</author>
// <date></date>
// <summary>API Provider</summary>
namespace PatientPortal.Provider.Models
{
    public static class APIProvider
    {
        private static string apiPrefix = $"api/";

        /// <summary>
        /// Dynamic transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="data"></param>
        /// <param name="urlApi"></param>
        /// <returns></returns>
        public static async Task<Q> DynamicTransaction<T, Q>(T data, string urlApi, string applicationTag = "cms")
        {
            Uri _baseAddress = URLBuilder.APIBaseAddress(applicationTag);
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));

                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, APIConstant.SerializeContentType);
                httpClient.BaseAddress = _baseAddress;

                HttpResponseMessage response = await httpClient.PostAsync(apiPrefix + urlApi, content);

                if (!response.IsSuccessStatusCode) return default(Q); //Check status code

                return response.Content.ReadAsAsync<Q>(new[] { new ProtoBufFormatter() }).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Q> Authorize_DynamicTransaction<T, Q>(T data,string token, string urlApi, string applicationTag = "cms", string ars = "")
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);
            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, APIConstant.SerializeContentType);
                    httpClient.BaseAddress = _baseAddress;

                    if (ars.Length < 1 && ars != null) ars = ARS.IgnoredARS;
                    if (ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    HttpResponseMessage response = await httpClient.PostAsync(apiPrefix + urlApi, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if (errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    return response.Content.ReadAsAsync<Q>(new[] { new ProtoBufFormatter() }).Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get data from API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlApi"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(string urlApi, string applicationTag = "cms")
        {
            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;

                    var response = await httpClient.GetAsync(apiPrefix + urlApi);

                    if (!response.IsSuccessStatusCode) return default(T); //Check status code

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null? default(T):data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static async Task<T> Get<T>(string urlApi, string action, object dynamicPara, string applicationTag = "cms")
        {
            urlApi += "/" + action + (dynamicPara == null? "":"/?" + URLBuilder.GetQueryString(dynamicPara));
            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;

                    var response = await httpClient.GetAsync(apiPrefix + urlApi);

                    if (!response.IsSuccessStatusCode) return default(T); //Check status code

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null ? default(T) : data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async Task<bool> Authorization(string token, string ars = null)
        {
            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_CORE);
            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;
                    if (ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    var response = await httpClient.GetAsync(apiPrefix + APIConstant.API_Authorize);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if (errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    return true;
                }
                catch (HttpException ex)
                {
                    // Logger.LogError(ex);
                    throw ex;
                }
            }
        }

        public static T Authorize_GetNonAsync<T>(string token, string urlApi, string applicationTag = "cms", string ars = null)
        {
            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);
            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;
                    if (ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    var response = httpClient.GetAsync(apiPrefix + urlApi).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if (errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null ? default(T) : data);
                }
                catch (HttpException ex)
                {
                    Logger.LogError(ex);
                    throw ex;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    throw ex;
                }
            }
        }

        public static async Task<T> Authorize_Get<T>(string token, string urlApi, string applicationTag = "cms", string ars = null)
        {
            Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);
            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;
                    if (ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    var response = await httpClient.GetAsync(apiPrefix + urlApi);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if(errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null ? default(T) : data);
                }
                catch (HttpException ex)
                {
                   Logger.LogError(ex);
                    throw ex;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    throw ex;
                }
            }
        }

        public static async Task<T> Authorize_Get<T>(string token, string urlApi, string action, object dynamicPara, string applicationTag = "cms", string ars = null)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            
            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    urlApi += "/" + action + (dynamicPara == null ? "" : "/?" + URLBuilder.GetQueryString(dynamicPara));
                    Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;
                    if(ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    var response = await httpClient.GetAsync(apiPrefix + urlApi);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if (errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null ? default(T) : data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T Authorize_GetNonAsync<T>(string token, string urlApi, string action, object dynamicPara, string applicationTag = "cms", string ars = null)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            using (var httpClient = HttpClientHelper.GetHttpClient(token))
            {
                try
                {
                    urlApi += "/" + action + (dynamicPara == null ? "" : "/?" + URLBuilder.GetQueryString(dynamicPara));
                    Uri _baseAddress = URLBuilder.APIBaseAddress("webapi:" + applicationTag);

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstant.ContentType));
                    httpClient.BaseAddress = _baseAddress;
                    if (ars != null && ars.Length > 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(APIConstant.XAuthorizeHeaderARS, ars);
                    }

                    var response = httpClient.GetAsync(apiPrefix + urlApi).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMess = response.Content.ReadAsStringAsync().Result;
                        if (errorMess.Length > 0)
                        {
                            Exception ex = new Exception(errorMess);
                            Logger.LogError(ex);
                        }
                        throw new HttpException((int)response.StatusCode, errorMess);
                    }

                    var data = response.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() }).Result;

                    return (data == null ? default(T) : data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// API Generator - Transaction
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string APIGenerator(string controllerName, string action)
        {
            return controllerName + "?action=" + action;

        }
        public static string APIGenerator(string controllerName, string method, string action)
        {
            return controllerName + "/" + method + (action != null && action.Length > 0 ? "?action=" + action : "");

        }


        /// <summary>
        /// API Genertor - Query
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="methodName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string APIGenerator(object controller, string methodName, params object[] para)
        {
            string parameterUrl = string.Empty;
            List<Type> types = new List<Type>();

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                parameterUrl += (i == 0 ? "/" : (i == 1? "?" : "&") + i.ToString().TrimEnd() + "=") + value;

                types.Add(value.GetType());
            }

            Type[] parasType = types.ToArray();

            var method = controller.GetType().GetMethod(methodName, parasType);
            if (method != null && method.GetParameters().Length > 0)
            {
                for (int i = 1; i < method.GetParameters().Length; i++)
                {
                    var name = method.GetParameters()[i].Name.TrimEnd() + "=";
                    var searchString = i.ToString().TrimEnd() + "=";
                    parameterUrl = parameterUrl.Replace(searchString, name);
                }
            }

            return parameterUrl;
        }


        public static string APIGenerator(string controllerName, string methodName, List<string> listParaName, bool isNamedParaFirst = false, params object[] para)
        {
            string parameterUrl = string.Empty;
            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listParaName[i];
                parameterUrl += (i == 0 ? ((name != "id"?"?":"/") + (isNamedParaFirst? name + "=": "")) : ((i == 1 && listParaName[0] == "id" ? "?" : "&") + name + "=")) + value;

            }

            return controllerName + "/" + (methodName.Length > 0? methodName :"") + parameterUrl;
        }

        public static string APIGenerator(string controllerName, List<string> listParaName, bool isNamedParaFirst = false, params object[] para)
        {
            string parameterUrl = string.Empty;
            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listParaName[i];
                parameterUrl += (i == 0 ? ((name.ToLower() != "id" ? "?" : "/") + (isNamedParaFirst ? name + "=" : "")) : (i == 1 && listParaName[0] == "id" ? "?" : "&") + name + "=") + value;

            }

            return controllerName + parameterUrl;
        }



        /// <summary>
        /// API Generator Parameters - Dynamic from API Controller
        /// </summary>
        /// <param name="listValue"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> APIGeneratorParameter(ICollection<string> listValue, params object[] para)
        {
            string parameterUrl = string.Empty;
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();
            var paraName = listValue.ToList();

            var lengthList = listValue.Count;
            var lengthPara = para.Length;
            var variance = lengthList - lengthPara;

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = paraName[i + variance];
                paraList.Add(name, value);
            }

            return paraList;
        }

        /// <summary>
        /// API Generator Parameters - Manual object from API Controller
        /// </summary>
        /// <param name="listValue"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> APIDefaultParameter(IList<string> listValue, params object[] para)
        {
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listValue[i];
                paraList.Add(name, value);
            }

            return paraList;
        }
        public static Dictionary<string, dynamic> APIDefaultParameter<T>(T data)
        {
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();

            foreach (var prop in data.GetType().GetProperties())
            {
                var name = prop.Name;
                var val = prop.GetValue(data, null);
                paraList.Add(name, val);
            }
            return paraList;
        }

    }
}