using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using GTAssignment.ApplicationClient;
using GTAssignment.Models;
using GTAssignment.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GTAssignment.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly ApiClient apiClient = new ApiClient();
        private HttpResponseMessage response = null;
        private readonly ScenarioContext scenarioContext;
        private User userData = null;
        public UserSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }


        [Given(@"I set api endpoint for user")]
        public void GivenISetApiEndPointForUser()
        {
            apiClient.SetUri("/user");
        }

        [When(@"I send post request with valid new user data")]
        public void WhenISendPostRequestWithValidNewUserData()
        {
            userData = TestDataHelper.GetValidUserData();

            response = apiClient.PostData(userData);

            scenarioContext.Add("NewUserAdded", userData);

        }

        [When(@"I send post request with no data")]
        public void WhenISendPostRequestWithInvalidNewUserData()
        {
            response = apiClient.PostData("\"\"");
        }

        [When(@"I send post request with invalid json data")]
        public void WhenISendPostRequestWithInvalidJsonData()
        {
            response = apiClient.PostData("'id':48591,'username':'qklbdluv''firstName':'bzdu'");
        }


        [Given(@"I already created user with user name (.*)")]
        public void GivenIAlreadyCreatedUserWithUserName(string userName)
        {
            userData = TestDataHelper.GetValidUserData();

            userData.username = userName;
            response = apiClient.PostData(userData);

            if (!response.IsSuccessStatusCode)
                throw new SpecFlowException("User not created, " + response.ReasonPhrase);

            scenarioContext.Add("NewUserAdded", userData);

        }


        [Then(@"I should get response status code as (.*)")]
        public void ThenIShouldGetResponseStatusCodeAs(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)response.StatusCode);
        }


        [When(@"I send get request with user name (.*)")]
        [When(@"I send get request with invalid user name (.*)")]
        public void WhenISendGetRequestWithValidUserName(string userName)
        {
            response = apiClient.GetData(userName);
        }

        [When(@"I send get request to get newly created user")]
        public void WhenISendGetRequestToGetNewlyCreatedUser()
        {
            userData = (User)scenarioContext["NewUserAdded"];
            response = apiClient.GetData(userData.username);
        }


        [When(@"I send delete request with valid user name (.*)")]
        [When(@"I send delete request with invalid user name (.*)")]
        public void WhenISendDeleteRequestWithValidUserName(string userName)
        {
            response = apiClient.DeleteData(userName);
        }

        [When(@"I send put request to update user details for user (.*)")]
        public void WhenISendPutRequestToUpdateUserDetails(string userName)
        {
            userData = (User)scenarioContext["NewUserAdded"];

            userData.firstName = "udatefname";
            userData.lastName = "udatelname";
            userData.phone = "8898877657";

            response = apiClient.PutData(userData, userName);
        }

        [When(@"I send put request to update user details for invalid user (.*)")]
        public void WhenISendPutRequestToUpdateUserDetailsWithInvalidUserName(string userName)
        {
            userData = new User
            {
                username = userName,
                firstName = "ivUserfname",
                lastName = "ivuserLname",
                phone = "6673355647"
            };

            response = apiClient.PutData(userData, userName);
        }

        [Then(@"I compare post data with get response data")]
        public void ThenIComparePostDataWithGetResponseData()
        {
            var postUserData = JsonConvert.SerializeObject((User)scenarioContext["NewUserAdded"]);
            var getUserData = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(postUserData, getUserData, "Expected:" + postUserData + " Actual:" + getUserData);
        }







    }
}
