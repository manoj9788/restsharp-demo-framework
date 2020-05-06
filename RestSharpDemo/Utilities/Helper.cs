using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpDemo.Utilities
{
    public static class Helper
    {

        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var jsonObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);
            return jsonObj;
        }
        
        public static string DeserializeResponseUsingJObject(this IRestResponse restResponse, string responseObj)
        {  
            var jObject = JObject.Parse(restResponse.Content);
            return jObject[responseObj]?.ToString();
        }
        
        public static bool IsScuccessStatusCode(this HttpStatusCode responseCode)
        {
            var numericResponse = (int)responseCode;

            const int statusCodeOk = (int)HttpStatusCode.OK;

            const int statusCodeBadRequest = (int)HttpStatusCode.BadRequest;

            return numericResponse >= statusCodeOk &&
                   numericResponse < statusCodeBadRequest;
        }
        
        
    }
}