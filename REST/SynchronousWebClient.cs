using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace WelcomeDAM.REST
{
    public class SynchronousWebClient
    {
        private string accessToken;

        public SynchronousWebClient()
        {
        }

        public bool HasAccessToken =>
            !string.IsNullOrEmpty(accessToken);

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public void ClearAccessToken()
        {
            this.accessToken = null;
        }

        public T Get<T>(string url) where T : class, new()
        {
            T data = null;

            HttpWebRequest request = CreateRequest(HttpMethod.Get, url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string responseJSON = myStreamReader.ReadToEnd();
                        data = JsonConvert.DeserializeObject<T>(responseJSON);
                    }
                }

                request = null;
            }

            return data;
        }

        public T Post<T>(string url) where T : class, new()
        {
            HttpWebRequest request = CreateRequest(HttpMethod.Post, url);

            request.ContentLength = 0;

            return ExecutePostRequest<T>(request);
        }

        public T Post<TBody, T>(string url, TBody body)
            where TBody : class, new()
            where T : class, new()
        {
            HttpWebRequest request = CreateRequest(HttpMethod.Post, url);

            string bodyJson = JsonConvert.SerializeObject(body);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(bodyJson);
            request.ContentType = "application/json";
            request.ContentLength = bodyBytes.Length;

            return ExecutePostRequest<T>(request, bodyBytes);
        }

        public T PostUrlEncoded<T>(string url, Dictionary<string, string> parameters) where T : class, new()
        {
            HttpWebRequest request = CreateRequest(HttpMethod.Post, url);

            string content = string.Join("&", parameters.Keys.Select(key => $"{key}={Uri.EscapeDataString(parameters[key])}"));
            byte[] bodyBytes = Encoding.UTF8.GetBytes(content);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bodyBytes.Length;

            return ExecutePostRequest<T>(request, bodyBytes);
        }

        public bool PostUrlEncodedAndCheckStatusCode(string url, Dictionary<string, string> parameters, HttpStatusCode statusCode)
        {
            HttpWebRequest request = CreateRequest(HttpMethod.Post, url);

            string content = string.Join("&", parameters.Keys.Select(key => $"{key}={Uri.EscapeDataString(parameters[key])}"));
            byte[] bodyBytes = Encoding.UTF8.GetBytes(content);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bodyBytes.Length;

            return ExecutePostRequestAndCheckStatusCode(request, bodyBytes, statusCode);
        }

        private HttpWebRequest CreateRequest(HttpMethod method, string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = method.ToString().ToUpper();

            if (HasAccessToken)
            {
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            }

            return request;
        }

        private T ExecutePostRequest<T>(HttpWebRequest request, byte[] body = null) where T : class, new()
        {
            T data = null;

            if (body != null)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(body, 0, body.Length);
                    requestStream.Close();
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string responseJSON = myStreamReader.ReadToEnd();
                        data = JsonConvert.DeserializeObject<T>(responseJSON);
                    }
                }

                request = null;
            }

            return data;
        }

        private bool ExecutePostRequestAndCheckStatusCode(HttpWebRequest request, byte[] body, HttpStatusCode statusCode)
        {
            if (body != null)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(body, 0, body.Length);
                    requestStream.Close();
                }
            }

            bool isSameStatusCode = false;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                isSameStatusCode = (response.StatusCode == statusCode);
            }

            return isSameStatusCode;
        }
    }
}