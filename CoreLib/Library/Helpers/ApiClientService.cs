
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public static class ApiClientService
    {
        const string API_URL = "http://localhost:5001";

        public static string ClientRequest(string action, string token = "", string method = "POST")
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string requestUrl = string.Format("{0}/{1}", API_URL, action);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = method; // istek tipi
                request.ContentType = "application/json";
                request.Timeout = -1;
                if (token != "")
                {
                    request.Headers.Add("Authorization", "Bearer " + token);
                }
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var str = reader.ReadToEnd();
                return str;
            }
            catch (WebException ex)
            {
                string message = "";

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response;
                    message = new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd().Trim();
                }
                return message;
            }
        }

        public static string ClientRequest<T>(T item, string action, string token = "", string method = "POST")
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(item);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
                //burayı incelenecek
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string requestUrl = string.Format("{0}/{1}", API_URL, action);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = method;
                request.ContentLength = byteArray.Length;
                request.ContentType = "application/json";
                request.Timeout = -1;

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add("Authorization", "Bearer " + token);
                }
                //request.KeepAlive = false;
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonString);
                }
                WebResponse response = null;
                response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var str = reader.ReadToEnd();
                return str;
            }
            catch (WebException ex)
            {
                string message = "";

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response;
                    message = new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd().Trim();
                }
                return message;
            }
        }
    }
}
