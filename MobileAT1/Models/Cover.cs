
using Newtonsoft.Json;

namespace MobileAT1.Models
{
    // Represents a video game cover url returned from IGDB.
    public class Cover
    {
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonIgnore]
        // If the url is null or empty use placeholder image.
        // Else if the url starts with double slashes add https.
        // As api returns the thumb nail replace with cover_big.
        public string ImageUrl =>
            string.IsNullOrWhiteSpace(Url)
                ? "cover_placeholder.svg"
                : (Url.StartsWith("//") ? "https:" + Url : Url)
                    .Replace("/t_thumb/", "/t_cover_big/");

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
