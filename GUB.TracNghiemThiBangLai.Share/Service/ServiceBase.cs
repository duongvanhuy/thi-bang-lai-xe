using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GUB.TracNghiemThiBangLai.Share.Service
{
    public  class ServiceBase 
    {

        private readonly RestClient _client;
        private static readonly string _baseUrl = "https://localhost:7233/api/";

       

        public ServiceBase()
        {
            _client = new RestClient(_baseUrl);
           
        }
        public async Task<T> CallAPIPost<T>(string endPonit, object data)
        {
            try
            {
                var resource = JsonConvert.SerializeObject(data);
                //  var client = new RestClient(_baseUrl);
                var requestQueryString = endPonit + GetQueryString(data);
                var request = new RestRequest(requestQueryString, Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(resource);

                
                /* var jwToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
                 if (!string.IsNullOrEmpty(jwToken))
                 {
                     request.AddHeader("Authorization", jwToken);
                 }*/
                var response = await _client.ExecuteAsync<T>(request);

              /*  var dataReponse = JsonConvert.DeserializeObject<T>(response.Content);*/

                return response.Data;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> CallAPIGet<T>(string endPonit, object data)
        {
            try
            {
                // var jwToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTWFuYWdlciIsImV4cCI6MTY2NzgxNDY2OSwiaXNzIjoiSGluZVRyYXZlbCIsImF1ZCI6IkhpbmVUcmF2ZWwifQ.SoR7TTFX73uj75uWXLXdSQ92kCujmD5MWrXXeINi2as";
                var resource = JsonConvert.SerializeObject(data);
                var requestQueryString = endPonit + GetQueryString(data);
                var request = new RestRequest(requestQueryString, Method.Get);
                
                /* var jwToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
                 if (!string.IsNullOrEmpty(jwToken))
                 {
                     request.AddHeader("Authorization", jwToken);
                     request.AddHeader("Authorization", $"Bearer {jwToken}");
                 }
 */
                var response = await _client.ExecuteAsync<T>(request);

                return response.Data;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private string GetQueryString(object obj)
        {
            if (obj != null)
            {
                var properties = from p in obj.GetType().GetProperties()
                                 where p.GetValue(obj, null) != null
                                 select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());
                var data = "?" + String.Join("&", properties.ToArray());
                return data;
            }
            return null;

        }
    }
}
