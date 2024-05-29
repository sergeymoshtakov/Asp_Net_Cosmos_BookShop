using Newtonsoft.Json;

namespace BookShop.Models
{
    public class Author
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
