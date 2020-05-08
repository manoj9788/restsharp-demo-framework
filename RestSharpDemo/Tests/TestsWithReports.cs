    using System;
    using System.Collections.Generic;
    using System.Net;
    using AventStack.ExtentReports;
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
        public class TestsWithReports
        {
            private IRestClient _restClient;
            private IRestRequest _restRequest;
            private IRestResponse _restResponse;
            private const string BaseUrl = "https://reqres.in/";
            
            [OneTimeSetUp]
            public void SetupReport()
            {
                var dir = TestContext.CurrentContext.TestDirectory;
                //var dir = TestContext.CurrentContext.TestDirectory; Or give a path where you'd want your test reports.
                
                Console.WriteLine(dir);
                Reporter.SetupExtentReport("RestSharp Demo Framework", "Extent Reports for API tests", dir);
            }
            
            [SetUp]
            public void Setup()
            {
                Reporter.CreateTest(TestContext.CurrentContext.Test.Name);
                _restClient = new RestClient(BaseUrl);
                
            }
            
            [Test]
            public void Test_01_SimpleGetRequestTest()
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
            public void Test_02_VerifyTotalNumberOfRecords()
            {
                _restRequest= new RestRequest("/api/users?page=2", Method.GET);

                _restResponse = _restClient.Execute(_restRequest);

                var result = _restResponse.DeserializeResponse()["total"];
                Assert.That(result, Is.EqualTo("12"), "Total Records are mismatch");
            }

            [Test]
            public void Test_03_GetRequestWithAuthenticationParams()
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
                var testStatus = TestContext.CurrentContext.Result.Outcome;
                Reporter.LogReport(Status.Info, "Test ended with status: " + testStatus.ToString());
            }
            [OneTimeTearDown]
            public void CloseForReport()
            {
                Reporter.FlushReport();
            }
            
        }
    }