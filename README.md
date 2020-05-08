# restsharp-api-tests

A demo project to showcase API testing in C# language using RestSharp package. 

### Dev Environment
This project is developed and tested on dotnet core 3.1 on Mac OSX with Jetbrains Rider as IDE

### Tests
The tests directory has different tests,
* [TestsWithGetOperation](/RestSharpDemo/Tests/TestsWithGetOperation.cs) - Tests showing usage of GET operation.
* [TestsWithMultipleData](/Tests/TestsWIthMultipleData.cs) - Tests showing usage of Data-Driven capability using NUnit and RestShap
* [TestsWithPostOperation](/RestSharpDemo/Tests/TestsWithPostOperation.cs) - Tests showing usage of POST operation.
* [TestsWithReports](/RestSharpDemo/Tests/TestWithReport.cs) - Tests showing integration of Extent Reports with RestSharp.

### Utilities
This project has two helper classes to assist write better tests,
* [Helper](/RestSharpDemo/Utilities/Helper.cs) - Helper Class to organize commonly used methods
* [Reporter](/RestSharpDemo/Utilities/Reporter.cs) - Reporter Class that has Extent Report package integration.

Please feel free to extend this boilerplate project as per your needs.

I'm no C# expert and tried to put together this project with my limited knowledge, please be gentle and contribute to make this project better.
