using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using GTAssignment.ApplicationClient;
using GTAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GTAssignment
{
    [Binding]
    public sealed class Hooks
    {
        private ScenarioContext scenarioContext;
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ScenarioContext ScenarioConext;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentHtmlReporter = new ExtentHtmlReporter(@"C:\Users\Lenovo\Documents\GitHub\GTAssignment\GTAssignment\index.html");
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
        }
        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {

            if (null != featureContext)
            {

                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            }
        }
        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioConext)
        {

            if (null != scenarioConext)
            {
                ScenarioConext = scenarioConext;
                _scenario = _feature.CreateNode<Scenario>(scenarioConext.ScenarioInfo.Title, scenarioConext.ScenarioInfo.Description);
            }
        }

        [AfterStep]
        public void AfterEachStep()
        {
            ScenarioBlock scenarioBlock = scenarioContext.CurrentScenarioBlock;

            switch (scenarioBlock)
            {

                case ScenarioBlock.Given:
                    if (scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<Given>(scenarioContext.TestError.Message).Fail("\n" + scenarioContext.TestError.StackTrace);

                    }
                    else
                    {
                        _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    }

                    break;
                case ScenarioBlock.When:
                    if (scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<When>(scenarioContext.TestError.Message).Fail("\n" + scenarioContext.TestError.StackTrace);

                    }
                    else
                    {
                        _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    }

                    break;
                case ScenarioBlock.Then:
                    if (scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<Then>(scenarioContext.TestError.Message).Fail("\n" + scenarioContext.TestError.StackTrace);

                    }
                    else
                    {
                        _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    }

                    break;
                default:
                    if (scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<And>(scenarioContext.TestError.Message).Fail("\n" + scenarioContext.TestError.StackTrace);

                    }
                    else
                    {
                        _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
                    }

                    break;




            }
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();

        }

        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            //TODO: implement logic that has to run before executing each scenario

        }

        [AfterScenario("RemoveUser")]
        public static void RemoveUser(ScenarioContext scenarioContext)
        {
            try
            {
                if (scenarioContext.ContainsKey("NewUserAdded"))
                {
                    var user = (User)scenarioContext["NewUserAdded"];

                    ApiClient apiClient = new ApiClient();
                    apiClient.SetUri("/user");
                    var response = apiClient.DeleteData(user.username);
                }
            }
            catch (Exception)
            { }
        }
    }
}
