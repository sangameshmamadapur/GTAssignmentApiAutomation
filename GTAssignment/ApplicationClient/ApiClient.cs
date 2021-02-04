using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GTAssignment.Models;
using GTAssignment.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace GTAssignment.ApplicationClient
{
    public class ApiClient
    {
        private string ApiURL;
        private static TestConfig config;

        public ApiClient()
        {
            config = ConfigReader.ParseConfig();
        }
        public void SetUri(string uri)
        {
            ApiURL = config.BaseURL + uri;
        }

        public HttpResponseMessage PostData(dynamic dataToPost)
        {
            try
            {
                using var client = new HttpClient();
                var jsonData = "";
                if (dataToPost is String)
                    jsonData = dataToPost;
                else
                    jsonData = JsonConvert.SerializeObject(dataToPost);

                var content = new StringContent(jsonData, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client.PostAsync(ApiURL, content).Result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public HttpResponseMessage GetData(string keyValue)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client.GetAsync(ApiURL + "/" + keyValue).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public HttpResponseMessage DeleteData(string keyValue)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client.DeleteAsync(ApiURL + "/" + keyValue).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public HttpResponseMessage PutData(dynamic dataToUpdate, string keyValue)
        {
            try
            {
                using var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(dataToUpdate), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client.PutAsync(ApiURL + "/" + keyValue, content).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

    }



}
