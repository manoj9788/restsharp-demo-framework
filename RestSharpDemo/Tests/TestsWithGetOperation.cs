    using System;
    using System.Collections.Generic;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using RestSharp;
    using RestSharp.Authenticators;
    using RestSharp.Serialization.Json;
    using RestSharpDemo.Utilities;

    namespace RestSharpDemo.Tests
    {
        [TestFixture]
        public class TestsWithGetOperation
        {
            private IRestClient _restClient;
            private IRestRequest _restRequest;
            private IRestResponse _restResponse;
            private const string BaseUrl = "https://reqres.in/";
            
            [SetUp]
            public void Setup()
            {
                 _restClient = new RestClient(BaseUrl);
                
            }
            
            
            [Test]
            public void SimpleGetRequestTest()
            {
                //Arrange
                _restRequest = new RestRequest("api/users/{user}", Method.GET);
                _restRequest.AddUrlSegment("user", 2);
                
                //Act
                _restResponse = _restClient.Execute(_restRequest);
               
                //Assert
                Console.WriteLine("**** This is the response **** "+ _restResponse.Content);
                Assert.That(_restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                
            }
            [Test]
            public void SimpleGetRequestWithHelperMethod()
            {
                //Arrange
                var restRequest = Helper.CreateGetRequest("api/users/2");
                
                //Act
                var output = Helper.GetResponse(_restClient, restRequest);
                
                //Assert
                Console.WriteLine("**** This is the response **** "+ output.Content);
                Assert.That(output.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                
            }
            
            [Test]
            public void VerifyTotalNumberOfRecords()
            {
                _restRequest= new RestRequest("/api/users?page=2", Method.GET);

                _restResponse = _restClient.Execute(_restRequest);

                var result = _restResponse.DeserializeResponse()["total"];
                Assert.That(result, Is.EqualTo("12"), "Total Records are mismatch");
            }

            [Test]
            public void GetRequestWithAuthenticationParams()
            {
                _restClient.Authenticator = new HttpBasicAuthenticator("eve.holt@reqres.in", "cityslicka");
                _restRequest = new RestRequest("https://reqres.in/api/login", Method.GET);

                _restResponse = _restClient.Execute(_restRequest);
                Assert.That(_restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            
            [TearDown]
            public void Close()
            {
                Console.WriteLine("Hey! I've done executing all the tests");
            }
            
        }
    }