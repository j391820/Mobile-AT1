
using Newtonsoft.Json;

namespace MobileAT1.Models
{
    public class Cover
    {
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonIgnore]
        public string ImageUrl =>
            Url.Replace("//", "https://").Replace("/t_thumb/", "/t_cover_big/");
           

        public Cover()
        {
            Url = string.Empty;
        }

        public Cover(string url)
        {
            Url = url;
        }
    }
}
