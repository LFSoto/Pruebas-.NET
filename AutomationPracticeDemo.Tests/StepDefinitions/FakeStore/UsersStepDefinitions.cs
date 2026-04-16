using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using NUnit.Framework;
using Reqnroll;

namespace AutomationPracticeDemo.Tests.StepDefinitions.FakeStore
{
    [Binding]
    public class UsersStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private FakeStoreClient _client = null!;
        private List<User>? _users;
        private User? _user;
        private bool _deleteSucceeded;

        public UsersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private FakeStoreClient GetClient()
        {
            if (_client is not null) return _client;
            _client = _scenarioContext.Get<FakeStoreClient>("apiClient");
            return _client;
        }

        // ?? Get all users ??

        [When(@"I query all users")]
        public async Task WhenIQueryAllUsers()
        {
            _users = await GetClient().GetAllUsersAsync();
        }

        [Then(@"the response should contain a list of users")]
        public void ThenTheResponseShouldContainAListOfUsers()
        {
            Assert.That(_users, Is.Not.Null.And.Not.Empty);
        }

        // ?? Get user by id ??

        [When(@"I query the user with id (.*)")]
        public async Task WhenIQueryTheUserWithId(int id)
        {
            _user = await GetClient().GetUserAsync(id);
        }

        [Then(@"the response should contain a user with id (.*)")]
        public void ThenTheResponseShouldContainAUserWithId(int id)
        {
            Assert.That(_user, Is.Not.Null);
            Assert.That(_user!.Id, Is.EqualTo(id));
        }

        // ?? Create a user ??

        [When(@"I create a user with username ""(.*)"" and email ""(.*)"" and password ""(.*)""")]
        public async Task WhenICreateAUserWithUsernameAndEmailAndPassword(string username, string email, string password)
        {
            var newUser = new User { Username = username, Email = email, Password = password };
            _user = await GetClient().CreateUserAsync(newUser);
        }

        [Then(@"the created user should have an id")]
        public void ThenTheCreatedUserShouldHaveAnId()
        {
            Assert.That(_user, Is.Not.Null);
            Assert.That(_user!.Id, Is.GreaterThan(0));
        }

        // ?? Update a user ??

        [When(@"I update user with id (.*) to username ""(.*)"" and email ""(.*)""")]
        public async Task WhenIUpdateUserWithIdToUsernameAndEmail(int id, string username, string email)
        {
            var updated = new User { Username = username, Email = email };
            _user = await GetClient().UpdateUserAsync(id, updated);
        }

        [Then(@"the updated user should have username ""(.*)""")]
        public void ThenTheUpdatedUserShouldHaveUsername(string username)
        {
            Assert.That(_user, Is.Not.Null);
            Assert.That(_user!.Username, Is.EqualTo(username));
        }

        // ?? Delete a user ??

        [When(@"I delete the user with id (.*)")]
        public async Task WhenIDeleteTheUserWithId(int id)
        {
            try
            {
                await GetClient().DeleteUserAsync(id);
                _deleteSucceeded = true;
            }
            catch
            {
                _deleteSucceeded = false;
            }
        }

        [Then(@"the user should be deleted successfully")]
        public void ThenTheUserShouldBeDeletedSuccessfully()
        {
            Assert.That(_deleteSucceeded, Is.True);
        }
    }
}
