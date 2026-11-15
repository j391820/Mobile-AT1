using MobileAT1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAT1.Services
{
    class ExchangeRateApiService
    {
        HttpClient _client = new HttpClient();

        /// <summary>
        /// Queries exchangerate api for current usd to aud exhange rate.
        /// </summary>
        /// <returns>Usd to Aud exchange rate.</returns>
        /// <exception cref="HttpRequestException">Unsuccessful status code. eg not 2xx.</exception>
        public async Task<double> GetExchangeRate()
        {
            string requestURL = " https://v6.exchangerate-api.com/v6/17beff29bed658661764b9c5/latest/USD";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestURL); ;
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server Responded: {response.StatusCode}");
            }

            string contentString = await response.Content.ReadAsStringAsync();
            // parse into a Jobject rather then model and build a exchange rate object.
            JObject data = JObject.Parse(contentString);
            // json returns a dictionary of conversion_rate aud, eur etc.
            // double index object to get only the aud.
            return (double)data["conversion_rates"]["AUD"];
        }
    }
}
