using RestSharp;
using System.Net;
using System.Text.Json;
using AutomationPracticeDemo.Tests.Tests.API.Reqres.Dtos;

namespace AutomationPracticeDemo.Tests.Tests.API.Reqres
{
    public class ReqresApiTest
    {
        private const string BaseUrl = "https://reqres.in";

        // Nota: Reqres usa un API key para evitar rate limits en algunos casos.
        // Se envía como header. Si el servicio cambia el nombre del header,
        // ajustarlo aquí.
        private const string ApiKeyHeaderName = "x-api-key";
        private const string ApiKey = "reqres_7562ee100a1e488bb32cd02032cc3932";

        private static RestClient CreateClient() => new(BaseUrl);

        private static RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader(ApiKeyHeaderName, ApiKey);
            request.AddHeader("Accept", "application/json");
            return request;
        }

        [Test]
        public async Task GetUsers_Page2_ShouldReturnUsers()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/users?page=2", Method.Get);

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<UsersListResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Page, Is.EqualTo(2));
            Assert.That(payload.Data, Is.Not.Null);
            Assert.That(payload.Data.Count, Is.GreaterThan(0));
            Assert.That(payload.Data.All(u => u.Id > 0), Is.True);
        }

        [Test]
        public async Task GetUnknown_ShouldReturnResourceList()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/unknown", Method.Get);

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<UnknownListResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Data, Is.Not.Null);
            Assert.That(payload.Data.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser2()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/users/2", Method.Get);

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<SingleUserResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Data, Is.Not.Null);
            Assert.That(payload.Data.Id, Is.EqualTo(2));
            Assert.That(payload.Data.Email, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task Register_ShouldReturnIdAndToken()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/register", Method.Post);
            request.AddJsonBody(new { email = "eve.holt@reqres.in", password = "pistol" });

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<AuthResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Id, Is.GreaterThan(0));
            Assert.That(payload.Token, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task Login_ShouldReturnToken()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/login", Method.Post);
            request.AddJsonBody(new { email = "eve.holt@reqres.in", password = "cityslicka" });

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<LoginResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Token, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task UpdateUser_ShouldReturnUpdatedAt()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/users/2", Method.Put);
            request.AddJsonBody(new { name = "morpheus", job = "zion resident" });

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var payload = JsonSerializer.Deserialize<UpdateUserResponse>(
                response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.Name, Is.EqualTo("morpheus"));
            Assert.That(payload.Job, Is.EqualTo("zion resident"));
            Assert.That(payload.UpdatedAt, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnNoContent()
        {
            var client = CreateClient();
            var request = CreateRequest("/api/users/2", Method.Delete);

            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), response.Content);
            Assert.That(string.IsNullOrWhiteSpace(response.Content), Is.True);
        }
    }
}
