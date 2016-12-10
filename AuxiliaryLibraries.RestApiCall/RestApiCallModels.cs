using System.Collections.Generic;

namespace AuxiliaryLibraries.RestApiCall
{
    public class RestApiModel
    {
        public string ApiBaseUrl { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, object> ParameterBody { get; set; }
    }

    //public class RestSharpModel : RestApiModel
    //{
    //    public string ApiRestUrl { get; set; }
    //    public RestSharp.Method ApiMethod { get; set; }
    //    /// <summary>
    //    /// If destionation expect username, you have to fill this property for Authentication
    //    /// </summary>
    //    public string UserName { get; set; }
    //    /// <summary>
    //    /// If destionation expect password, you have to fill this property for Authentication
    //    /// </summary>
    //    public string Password { get; set; }
    //}

    public class WebClientModel : RestApiModel
    {
        public string Method { get; set; }
    }
}
