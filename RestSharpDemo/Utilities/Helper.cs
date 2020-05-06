using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        
        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving response.";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }

                taskCompletionSource.SetResult(restResponse);
            });

            return await taskCompletionSource.Task;
        }
        
        
    }
}