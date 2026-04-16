using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Api.Dtos;
using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Api.Client
{
    public class FakeStoreClient
    {
        private readonly HttpClient _httpClient;

        public FakeStoreClient(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        // ?? Products ??????????????????????????????????????????????

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("/products");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(json) ?? [];
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/products/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(json)!;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/products", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(json)!;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/products/{id}", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(json)!;
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/products/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ?? Carts ?????????????????????????????????????????????????

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            var response = await _httpClient.GetAsync("/carts");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Cart>>(json) ?? [];
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/carts/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cart>(json)!;
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/carts", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cart>(json)!;
        }

        public async Task<Cart> UpdateCartAsync(int id, Cart cart)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/carts/{id}", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cart>(json)!;
        }

        public async Task DeleteCartAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/carts/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ?? Users ?????????????????????????????????????????????????

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("/users");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<User>>(json) ?? [];
        }

        public async Task<User> GetUserAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/users/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(json)!;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/users", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(json)!;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/users/{id}", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(json)!;
        }

        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/users/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ?? Auth ??????????????????????????????????????????????????

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/auth/login", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(json)!;
        }
    }
}
