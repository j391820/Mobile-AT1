using Newtonsoft.Json;
using MobileAT1.Models;

namespace MobileAT1.Services
{
    internal class IGDBApiService
    {
        HttpClient _client = new HttpClient();
        const string baseURL = "https://api.igdb.com/v4/";


        public async Task<List<Genre>> GetGenre()
        {
            List<Genre> genre = [];
            string requestURL = "https://api.igdb.com/v4/genres";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent("fields name; limit 50;");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }
     
            string contentString = await response.Content.ReadAsStringAsync();
            genre = JsonConvert.DeserializeObject<List<Genre>>(contentString) ?? [];

            return genre;
        }

        public async Task<List<Game>> GetAllGames()
        {
            List<Game> games = [];
            string requestURL = "https://api.igdb.com/v4/games";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent("fields name, cover.url;");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<List<Game>>(contentString) ?? [];

            return games;
        }

        public async Task<List<Game>> GetGamesByGenre(string genre)
        {
            List<Game> games = [];
            string requestURL = "https://api.igdb.com/v4/games";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent($"""fields name, cover.url; where genres.name = "{genre}";""");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<List<Game>>(contentString) ?? [];

            return games;
        }

        public async Task<Game> GetGameById(int id)
        {
            List<Game> games = [];
            string requestURL = "https://api.igdb.com/v4/games";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent($"fields name, storyline, cover.url, genres.name, platforms.name, game_modes.name; where id = {id};");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<List<Game>>(contentString) ?? [];
            Game game = games?.FirstOrDefault();

            return game!;
        }
    }
}
