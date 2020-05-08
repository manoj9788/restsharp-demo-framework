using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework.Internal;

namespace RestSharpDemo.Utilities
{
    public static class Reporter
    {
        public static ExtentReports ExtentReports;
        //public static ExtentReports extent;
        public static ExtentHtmlReporter ExtentHtmlReporter;
        public static ExtentTest TestCase;


        public static void SetupExtentReport(string reportName, string docTitle, dynamic path)
        {
            ExtentHtmlReporter = new ExtentHtmlReporter(path);
            ExtentHtmlReporter.Config.Theme = Theme.Dark;
            ExtentHtmlReporter.Config.DocumentTitle = docTitle;
            ExtentHtmlReporter.Config.ReportName = reportName;

            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(ExtentHtmlReporter);
        }

        public static void CreateTest(string testName)
        {
            TestCase = ExtentReports.CreateTest(testName);
        }

        public static void LogReport(Status status, string message)
        {
            TestCase.Log(status, message);
        }
        public static void FlushReport()
        {
            ExtentReports.Flush();
        }
        public static void TestStatus(string status)
        {
            if (status.Equals("Pass"))
            {
                TestCase.Pass("Test Passed");
            }
            else
            {
                TestCase.Fail("Test is Failed");
            }
        }
    }
}