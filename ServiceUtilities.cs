using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sample.Integration.Test
{
    public class ServiceUtilities
    {
        private static WebRequest GetRequest(string method, string contentType, string endPoint, string token = null, string content = null)
        {
            var request = WebRequest.Create(endPoint);
            request.Method = method;
            request.ContentType = contentType;

            if (token != null)
            {
                request.Headers.Add("Authorization-Token", token);
            }

            if (content != null)
            {
                var dataArray = Encoding.UTF8.GetBytes(content.ToString());
                request.ContentLength = dataArray.Length;
                var requestStream = request.GetRequestStream();
                requestStream.Write(dataArray, 0, dataArray.Length);
                requestStream.Flush();
                requestStream.Close();
            }

            return request;
        }

        private static string UnPack(WebResponse response)
        {
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }


        public static void CallService(RequestModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var response = GetRequest("POST", "application/json", "URL", null, data).GetResponse() as HttpWebResponse;
        }
    }
}
