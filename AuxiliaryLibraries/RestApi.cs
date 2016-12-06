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
    public static class RestApi
    {
        public static IRestResponse Send(this RestSharpModel apiModel)
        {
            var client = new RestClient(apiModel.ApiBaseUrl);
            if (!string.IsNullOrEmpty(apiModel.UserName) && !string.IsNullOrEmpty(apiModel.Password))
            {
                client.Authenticator = new HttpBasicAuthenticator(apiModel.UserName, apiModel.Password);
            }

            var request = new RestRequest(apiModel.ApiRestUrl, apiModel.ApiMethod);

            request.RequestFormat = DataFormat.Json;
            foreach (var header in apiModel.Headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            foreach (var parameter in apiModel.ParameterBody)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }
            // execute the request
            var response = client.Execute(request);
            // var content = response.Content; // raw content as string
            return response;
        }

        public static string Send(this WebClientModel apiModel)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiModel.ApiBaseUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = apiModel.Method;
                foreach (var header in apiModel.Headers)
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }

                if (apiModel.ParameterBody.Any())
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = GenerateBody(apiModel.ParameterBody);
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
