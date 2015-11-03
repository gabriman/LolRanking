using Newtonsoft.Json;

namespace FirstSPA.Models
{
    public class Champion
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]

        public string Name { get; set; }

        [JsonProperty("img")]
        public string ImageUrl { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}