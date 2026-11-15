using MobileAT1.Models;
using Newtonsoft.Json;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace MobileAT1.Services
{
    public class IGDBApiService
    {
        HttpClient _client = new HttpClient();
        const string baseURL = "https://api.igdb.com/v4/";

        /// <summary>
        /// Queries IGDB to get all of its genres.
        /// </summary>
        /// <returns>List of genre objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
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
            // newtonsoft json deserializer automatically handles turning the json into actual objects
            genre = JsonConvert.DeserializeObject<List<Genre>>(contentString) ?? [];

            return genre;
        }

        /// <summary>
        /// Queries IGDB to get all of its games.
        /// </summary>
        /// <returns>list of game objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
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

        /// <summary>
        /// Queries IGDB to get games in 50 title chunks.
        /// </summary>
        /// <param name="offset">Start point from total list of titles.</param>
        /// <returns>list of game objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
        public async Task<List<Game>> GetGames(int offset)
        {
            List<Game> games = [];
            string requestURL = "https://api.igdb.com/v4/games";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent($"fields name, cover.url; limit 50; offset {offset};");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<List<Game>>(contentString) ?? [];
            
            return games;
        }

        /// <summary>
        /// Converts game name into sentence case.
        /// </summary>
        /// <param name="name">name of game.</param>
        /// <returns>string</returns>
        public string NormaliseName(string name)
        {
            string result;
            if (!string.IsNullOrWhiteSpace(name))
            {
                string lowerName = name.ToLower(); 
                string[] words = lowerName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<string> capitalisedWords = new List<string>();
                foreach (string word in words)
                {
                    string capitalisedWord = char.ToUpper(word[0]) + word.Substring(1);
                    capitalisedWords.Add(capitalisedWord);
                }
                result = string.Join(" ", capitalisedWords);
            }
            else
            {
                result = name;
            }
            return result;
        }

        /// <summary>
        /// Queries IGDB to get all games where searched name is a match.
        /// </summary>
        /// <param name="name">name of game.</param>
        /// <returns>list of game objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
        public async Task<List<Game>> SearchGame(string name)
        {
            string normalisedName = NormaliseName(name);
            List<Game> games = [];
            string requestURL = "https://api.igdb.com/v4/games";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Client-ID", "wvfmte3qi2sg46n53xesd5nvzb3jxu");
            request.Headers.Add("Authorization", "Bearer g1ztgtew6a0w56ie3xmcxhnp73de3s");
            request.Content = new StringContent($"""fields name, cover.url; where name = "{normalisedName}";""");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<List<Game>>(contentString) ?? [];

            return games;
        }

        /// <summary>
        /// Queries IGDB to get all games where searched genre is a match.
        /// </summary>
        /// <param name="genre">game genre.</param>
        /// <returns>list of game objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
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

        /// <summary>
        /// Queries IGDB to get game where searched id is a match.
        /// </summary>
        /// <param name="id">IGDB game id.</param>
        /// <returns>list of game objects.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
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
            // Grab first match
            Game game = games?.FirstOrDefault();

            return game!;
        }
    }
}
