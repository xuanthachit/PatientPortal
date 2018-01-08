﻿using PatientPortal.Provider.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Client;

namespace PatientPortal.Provider.Models
{
    public class AuthenAPIHelper
    {

        public static TokenResponse GetToken(string username, string password)
        {

            var client = new OAuth2Client(
                new Uri(AppConfig.HostAddress_API_Authorize + "token"),
                ConfigurationManager.AppSettings.Get("ClientId"),
                string.Empty);
            try
            {
                var response = client.RequestResourceOwnerPasswordAsync(username, password).Result;
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static TokenResponse RefreshToken(string refreshToken)
        {
            var client = new OAuth2Client(
                new Uri(AppConfig.HostAddress_API_Authorize + "token"),
                ConfigurationManager.AppSettings.Get("ClientId"),
                string.Empty);

            var response = client.RequestRefreshTokenAsync(refreshToken).Result;
            return response;
        }
        //public static async Task<T> GetServiceAsync<T>(string token, string actionUrl, EnumApiUrlType type = EnumApiUrlType.AppApi) where T : class
        //{
        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(AppConfig.HostAddress_API_CMS + actionUrl)
        //        : new Uri(AppConfig.HostAddress_API_Authorize + actionUrl);
        //    var response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<T>();
        //    }
        //    return null;
        //    //return JsonConvert.DeserializeObject<T>(response);
        //}
        //public static async Task<T> PostServiceAsync<T>(string token, string actionUrl, T value, EnumApiUrlType type = EnumApiUrlType.AppApi) where T : class
        //{
        //    var AuthenApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_Authencation);
        //    var WebApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_CMS);

        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(WebApiUrl + actionUrl)
        //        : new Uri(AuthenApiUrl + actionUrl);
        //    var response = await client.PostAsJsonAsync(path, value);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<T>();
        //    }
        //    return null;
        //}
        //public static async Task<T> PutServiceAsync<T>(string token, string actionUrl, T value, EnumApiUrlType type = EnumApiUrlType.AppApi) where T : class
        //{
        //    var AuthenApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_Authencation);
        //    var WebApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_CMS);

        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(WebApiUrl + actionUrl)
        //        : new Uri(AuthenApiUrl + actionUrl);
        //    var response = await client.PutAsJsonAsync(path, value);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<T>();
        //    }
        //    return null;
        //}
        //public static async Task<HttpStatusCode> DeleteServiceAsync(string token, string actionUrl, EnumApiUrlType type = EnumApiUrlType.AppApi)
        //{
        //    var AuthenApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_Authencation);
        //    var WebApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_CMS);

        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(WebApiUrl + actionUrl)
        //        : new Uri(AuthenApiUrl + actionUrl);
        //    HttpResponseMessage response = await client.DeleteAsync(path);
        //    return response.StatusCode;
        //}
        //public static async Task<bool> PostServiceListAsync<T>(string token, string actionUrl, List<T> value, EnumApiUrlType type = EnumApiUrlType.AppApi) where T : class
        //{
        //    var AuthenApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_Authencation);
        //    var WebApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_CMS);

        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(WebApiUrl + actionUrl)
        //        : new Uri(AuthenApiUrl + actionUrl);
        //    var response = await client.PostAsJsonAsync(path, value);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;// await response.Content.ReadAsAsync<bool>();
        //    }
        //    return false;
        //}
        //public static async Task<bool> PutServiceListAsync<T>(string token, string actionUrl, List<T> value, EnumApiUrlType type = EnumApiUrlType.AppApi) where T : class
        //{
        //    var AuthenApiUrl = URLBuilder.APIBaseAddress("webapi:" + APIConstant.API_Resource_Authencation);
        //    var WebApiUrl = URLBuilder.APIBaseAddress("webapi:c" + APIConstant.API_Resource_CMS);

        //    var client = new HttpClient();
        //    client.SetBearerToken(token);
        //    var path = type == EnumApiUrlType.AppApi
        //        ? new Uri(WebApiUrl + actionUrl)
        //        : new Uri(AuthenApiUrl + actionUrl);
        //    var response = await client.PutAsJsonAsync(path, value);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<bool>();
        //    }
        //    return false;
        //}

    }
    //public enum EnumApiUrlType
    //{
    //    AppApi,
    //    AuthenApi
    //}
}
