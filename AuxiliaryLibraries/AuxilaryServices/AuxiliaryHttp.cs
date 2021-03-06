using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AuxiliaryLibraries.Extentions;

namespace AuxiliaryLibraries.AuxilaryServices
{
    /// <summary>
    /// AuxiliaryHttp helps you to handel Http requests and Response
    /// </summary>
    public static class AuxiliaryHttp
    {
        /// <summary>
        /// Fetch Requests (HttpRequestMessage) as List of object
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<object> Read(this System.Net.Http.HttpRequestMessage Request)
        {
            System.Collections.Generic.List<object> Response = new System.Collections.Generic.List<object>();
            var header = Request.Headers.GetEnumerator();
            while (header.MoveNext())
            {
                Response.Add(new { Key = header.Current.Key.ToString(), Value = string.Join(",", header.Current.Value) });
            }
            return Response;
        }

        /// <summary>
        /// Check the user request sent from mobile or not
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileRequest()
        {
            System.Diagnostics.Debug.Assert(HttpContext.Current != null);
            var result = false;
            var doubleCheck = HttpContext.Current.Request.Browser.IsMobileDevice;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                var httpUserAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();
                result = httpUserAgent.IsMobileRequest();
            }

            return result || doubleCheck;
        }

        /// <summary>
        /// Check the user request sent from mobile or not
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileRequest(this string httpUserAgent)
        {
            if (httpUserAgent.Length < 4)
                return false;

            if (AuxiliaryRegexPatterns.MobileRequestCheck.IsMatch(httpUserAgent) || AuxiliaryRegexPatterns.MobileVersionRequestCheck.IsMatch(httpUserAgent.Substring(0, 4)) || AuxiliaryRegexPatterns.MobileRequest.IsMatch(httpUserAgent))
                return true;

            return false;
        }
    }
}