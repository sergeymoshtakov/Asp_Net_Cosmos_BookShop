using Newtonsoft.Json;

namespace BookShop.Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "price")]
        public float Price { get; set; }
        [JsonProperty(PropertyName = "author_id")]
        public Guid Author_ID { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Decription {  get; set; }

        public Author Author { get; set; }

    }
}
