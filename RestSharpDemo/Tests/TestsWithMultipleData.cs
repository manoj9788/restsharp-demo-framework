using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo.Tests
{
    public class TestsWithData
    {
        [TestFixture]
        public class Tests
        {
            private IRestClient _restClient;
            private IRestRequest _restRequest;
            private IRestResponse _restResponse;
            private const string BaseUrl = "http://api.zippopotam.us";

            [SetUp]
            public void Setup()
            {    
                _restClient = new RestClient(BaseUrl);

            }

            [TestCase("IN", "110001", HttpStatusCode.OK, TestName = "Check status code for IN with pin code 110001")]
            [TestCase("AU", "2140", HttpStatusCode.OK, TestName = "Check status code for AU with pin code 2140")]
            [TestCase("IN", "909090909090909", HttpStatusCode.NotFound, TestName = "Check Not Found status code for IN with pin code 909090909090909")]
            public void TestWithPostCal_withData(string countryCode, string pinCode,
                HttpStatusCode expectedHttpStatusCode)
            {
                //http://api.zippopotam.us/us/90210

                _restRequest = new RestRequest($"{countryCode}/{pinCode}", Method.GET);

                _restResponse = _restClient.Execute(_restRequest);
                // Console.WriteLine("***"+result.Content);

                Assert.That(_restResponse.StatusCode, Is.EqualTo(expectedHttpStatusCode));
            }


            [Test,TestCaseSource(nameof(PlacesTestData))]
            public void Test_withTestCaseSourceData(string countryCode, string pinCode, string placeName)
            {
                _restRequest = new RestRequest($"{countryCode}/{pinCode}", Method.GET);
               
                _restResponse = _restClient.Execute(_restRequest);
                var result = new JsonDeserializer().Deserialize<Location>(_restResponse);
                
                // var output = response.DeserializeResponse();
                Console.WriteLine("*****" + result.Places[0].PlaceName);
               
                Assert.That(result.Places[0].PlaceName, Is.EqualTo(placeName));
            }

            private static IEnumerable<TestCaseData> PlacesTestData()
            {
                yield return new TestCaseData("au", "2140", "Homebush").SetName(
                    "Check status code for 2140 that has place name as Homebush");
                yield return new TestCaseData("au", "2140", "Homebush").SetName(
                    "Check status code for 2140 that has place name as Homebush");
                yield return new TestCaseData("au", "2140", "Homebush").SetName(
                    "Check status code for 2140 that has place name as Homebush");

            }
        }
    }
}