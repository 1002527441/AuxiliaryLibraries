using AuxiliaryLibraries.Extentions;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Calling api with RestSharp and WebClient easily
    /// </summary>
    public static class AuxiliaryRestApi
    {
        /// <summary>
        /// Send your request with RestSharp
        /// </summary>
        /// <param name="baseUrl">Base address of api url</param>
        /// <param name="functionName">Funcation name of api url</param>
        /// <param name="method">Method (POST, GET, PUT, PATCH, DELETE, COPY, HEAD, OPTIONS, LINK, UNLINK, PURGE, LOCK, UNLOCK, PROPFIND, VIEW)</param>
        /// <param name="headers">Headers (This is optional)</param>
        /// <param name="parametersBody">Body parameters (This is optional)</param>
        /// <param name="userName">Username (This is optional)</param>
        /// <param name="password">Password (This is optional)</param>
        /// <returns>IRestResponse</returns>
        public static IRestResponse Send(string baseUrl, string functionName, Method method, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string userName = null, string password = null)
        {
            try
            {
                if(string.IsNullOrEmpty(baseUrl))
                {
                    return new RestResponse()
                    {
                        ErrorMessage = "baseUrl could not be empty",
                        ErrorException = new NullReferenceException("baseUrl could not be empty")
                    };
                }
                var client = new RestClient(baseUrl);
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    client.Authenticator = new HttpBasicAuthenticator(userName, password);
                }

                var request = new RestRequest(functionName, method);

                request.RequestFormat = DataFormat.Json;
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                if (parametersBody != null)
                {
                    foreach (var parameter in parametersBody)
                    {
                        request.AddParameter(parameter.Key, parameter.Value);
                    }
                }
                // execute the request
                var response = client.Execute(request);
                // var content = response.Content; // raw content as string
                return response;
            }
            catch(Exception e)
            {
                return new RestResponse() {
                    ErrorException = e
                };
            }
        }

        /// <summary>
        /// Send your request with RestSharp
        /// </summary>
        /// <param name="baseUrl">Base address of api url</param>
        /// <param name="functionName">Funcation name of api url</param>
        /// <param name="method">Method (POST, GET, PUT, PATCH, DELETE, COPY, HEAD, OPTIONS, LINK, UNLINK, PURGE, LOCK, UNLOCK, PROPFIND, VIEW)</param>
        /// <param name="headers">Headers (This is optional)</param>
        /// <param name="body">Body as an object (This is optional)</param>
        /// <param name="userName">Username (This is optional)</param>
        /// <param name="password">Password (This is optional)</param>
        /// <returns>IRestResponse</returns>
        public static IRestResponse Send(string baseUrl, Method method, IDictionary<string, string> headers, object body)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(method);

            request.RequestFormat = DataFormat.Json;
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            if (body != null)
            {
                
                var json = new JavaScriptSerializer().Serialize(body); //JsonConvert.SerializeObject(body);
                request.AddHeader("Content-Length", json.Length.ToString());
                request.AddBody(body);
            }
            // execute the request
            var response = client.Execute(request);
            // var content = response.Content; // raw content as string

            return response;
        }

        /// <summary>
        /// Send your request with WebClient
        /// </summary>
        /// <param name="url">Address of api url</param>
        /// <param name="headers">Headers (This is optional)</param>
        /// <param name="parametersBody">Body parameters (This is optional)</param>
        /// <param name="method">Method ("POST", "GET", "PUT", "PATCH", "DELETE", "COPY", "HEAD", "OPTIONS", "LINK", "UNLINK", "PURGE", "LOCK", "UNLOCK", "PROPFIND", "VIEW") default value is "GET"</param>
        /// <param name="contentLength">If you don't want to fill Content-Length leave containContentLength as fasle.</param>
        /// <returns>string</returns>
        public static string Send(string url, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, Method method = Method.GET, bool contentLength = true)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    return "baseUrl could not be empty";
                
                var _method = GetMethod(method);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = _method;

                if (headers != null && parametersBody.Any())
                    foreach (var header in headers)
                        httpWebRequest.Headers.Add(header.Key, header.Value);

                string json = GenerateBody(parametersBody);
                if (!string.IsNullOrEmpty(json))
                {
                    //var byteArray = Encoding.UTF8.GetBytes(apiModel.Body);
                    //httpWebRequest.GetRequestStream().Write(byteArray, 0, byteArray.Length);
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                if (contentLength)
                    httpWebRequest.ContentLength = string.IsNullOrEmpty(json) ? 0 : json.Length;

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private static string GetMethod(this Method method)
        {
            var _method = method.ToString();
            if (!new List<string>() { "POST", "GET", "PUT", "PATCH", "DELETE", "COPY", "HEAD", "OPTIONS", "LINK", "UNLINK", "PURGE", "LOCK", "UNLOCK", "PROPFIND", "VIEW" }.Contains(_method.ToUpper()))
                throw new Exception("Method is not valid");
            return _method;
        }

        private static string GenerateBody(IDictionary<string, object> parametersBody)
        {
            if (parametersBody != null && parametersBody.Any())
            {
                //var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer(); javaScriptSerializer.Serialize(employee);
                var json = "{";
                int index = 1;
                foreach (var parameter in parametersBody)
                {
                    json += $"\"{parameter.Key}\": \"{parameter.Value}\"";
                    if (index < parametersBody.Count)
                    {
                        json += ",";
                    }
                    index++;
                }
                return json += "}";
            }
            return string.Empty;
        }
    }
}