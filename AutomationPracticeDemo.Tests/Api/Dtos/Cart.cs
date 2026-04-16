using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Api.Dtos
{
    public class CartProduct
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }

    public class Cart
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("products")]
        public List<CartProduct>? Products { get; set; }
    }
}
