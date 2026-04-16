using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using NUnit.Framework;
using Reqnroll;

namespace AutomationPracticeDemo.Tests.StepDefinitions.FakeStore
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private FakeStoreClient _client = null!;
        private LoginResponse? _loginResponse;

        public AuthStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private FakeStoreClient GetClient()
        {
            if (_client is not null) return _client;
            _client = _scenarioContext.Get<FakeStoreClient>("apiClient");
            return _client;
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public async Task WhenILoginWithUsernameAndPassword(string username, string password)
        {
            var request = new LoginRequest { Username = username, Password = password };
            _loginResponse = await GetClient().LoginAsync(request);
        }

        [Then(@"the response should contain a token")]
        public void ThenTheResponseShouldContainAToken()
        {
            Assert.That(_loginResponse, Is.Not.Null);
            Assert.That(_loginResponse!.Token, Is.Not.Null.And.Not.Empty);
        }
    }
}
