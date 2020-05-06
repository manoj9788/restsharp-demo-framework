using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo.Tests
{
        [TestFixture]
        public class TestsWithPostOperation
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
            //Test with Anonymous Body
            [Test]
            public void TestWithPostCall()
            {
                //Arrange
                _restRequest = new RestRequest("api/users" , Method.POST);
                _restRequest.AddJsonBody(new {name = "Tester", job = "Director" });

                //Act
                _restResponse  = _restClient.Execute(_restRequest);
                Console.WriteLine("Printing results for fun : "+_restResponse.Content);

                //Assert
                var result = _restResponse.DeserializeResponse();
                Assert.That(result["name"], Is.EqualTo("Tester"));
            }
            
            //Test POST operation with Type class
            [Test]
            public void TestPostWithTypeClass_Generics()
            {
                _restRequest = new RestRequest("api/register", Method.POST);
                _restRequest.AddJsonBody(new Users() {email = "eve.holt@reqres.in", password = "pistol"});

                var response = _restClient.Execute<Users>(_restRequest);

                Console.WriteLine("*** Response is *** " + response.Content);
                
                Assert.That(response.Data.token, Is.EqualTo("QpwL5tke4Pnpja7X4").IgnoreCase);
            }
        }
    
}