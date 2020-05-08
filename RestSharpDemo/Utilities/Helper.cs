using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpDemo.Utilities
{
    public static class Helper
    {
        private static BasicHttpBinding _basicHttpBinding;
        private static IRestRequest _restRequest;
        private static IRestResponse _restResponse;

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

        public static bool IsSuccessStatusCode(this HttpStatusCode responseCode)
        {
            var numericResponse = (int) responseCode;

            const int statusCodeOk = (int) HttpStatusCode.OK;

            const int statusCodeBadRequest = (int) HttpStatusCode.BadRequest;

            return numericResponse >= statusCodeOk &&
                   numericResponse < statusCodeBadRequest;
        }

        //You could even wrap the API calls as helper methods as below. Use it as extension methods at your tess.
        //Experimental.

        //CreateGetRequest method here accepts an optional String, that means,
        //you could  send an endpoint as string given the RestClient takes just the baseUrl otherwise considered null
        public static IRestRequest CreateGetRequest(string optionalEndPoint = null)
        {
            _restRequest = new RestRequest(optionalEndPoint, Method.GET);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }

        public static IRestRequest CreatePostRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.POST);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public static IRestRequest CreatePutRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PUT);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public static IRestRequest CreatePatchRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PATCH);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public static IRestRequest CreateDeleteRequest()
        {
            _restRequest = new RestRequest(Method.DELETE);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }

        public static IRestResponse GetResponse(IRestClient restClient, IRestRequest request)
        {
            return restClient.Execute(request);
        }

        public static BasicHttpBinding GetHttpBinding()
        {
            _basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            _basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            return _basicHttpBinding;
        }

        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request)
            where T : class, new()
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