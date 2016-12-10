using AuxiliaryLibraries.Extentions;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Calling api with RestSharp and WebClient easily
    /// </summary>
    public static class RestApi
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
        public static IRestResponse Send(string baseUrl, string functionName, Method method, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string userName, string password)
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
        /// Send your request with WebClient
        /// </summary>
        /// <param name="url">Address of api url</param>
        /// <param name="headers">Headers (This is optional)</param>
        /// <param name="parametersBody">Body parameters (This is optional)</param>
        /// <param name="method">Method ("POST", "GET", "PUT", "PATCH", "DELETE", "COPY", "HEAD", "OPTIONS", "LINK", "UNLINK", "PURGE", "LOCK", "UNLOCK", "PROPFIND", "VIEW") default value is "GET"</param>
        /// <returns>string</returns>
        public static string Send(string url, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string method = "GET")
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return "baseUrl could not be empty";
                }
                if(!new List<string>() { "POST", "GET", "PUT", "PATCH", "DELETE", "COPY", "HEAD", "OPTIONS", "LINK", "UNLINK", "PURGE", "LOCK", "UNLOCK", "PROPFIND", "VIEW" }.Contains(method.ToUpper()))
                {
                    return "method is not valid";
                }
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = method;

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpWebRequest.Headers.Add(header.Key, header.Value);
                    }
                }

                if (parametersBody != null && parametersBody.Any())
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = GenerateBody(parametersBody);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

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

        private static string GenerateBody(IDictionary<string, object> parameterBody)
        {
            //var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer(); javaScriptSerializer.Serialize(employee);
            var json = "{";
            int index = 1;
            foreach (var parameter in parameterBody)
            {
                json += $"\"{parameter.Key}\": \"{parameter.Value}\"";
                if (index < parameterBody.Count)
                {
                    json += ",";
                }
                index++;
            }
            return json += "}";
        }
    }
}
