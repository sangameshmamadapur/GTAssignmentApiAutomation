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
