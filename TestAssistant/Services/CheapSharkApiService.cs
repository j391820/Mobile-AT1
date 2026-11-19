using MobileAT1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MobileAT1.Services
{
    class CheapSharkApiService
    {
        HttpClient _client = new HttpClient();
        public CheapSharkApiService()
        {
            // API requested a user agent, otherwise said requests may be blocked. 
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("MobileAT1/1.0 (j391820@tafe.wa.edu.au)");
        }

        /// <summary>
        /// Converts game name into upper case.
        /// </summary>
        /// <param name="Title">name of game.</param>
        /// <returns>string</returns>
        public string capitaliseTitle(string Title)
        {
            // API has an internalName which is uppercase which can be searched for. 
            string formattedTitle = "";
            // Loop through every letter in supplied title.
            // Check if its a letter or number.
            // Capitalise them.
            foreach (char character in Title)
            {
                if (char.IsLetterOrDigit(character))
                {
                    formattedTitle += char.ToUpperInvariant(character);
                }
            }
            return formattedTitle;
        }



        /// <summary>
        /// Finds cheapest deal from list of deals.
        /// </summary>
        /// <param name="deals">List of game deals.</param>
        /// <returns>Cheapest deal from list.</returns>
        public Deal FindCheapestDeal(List<Deal> deals)
        {
            // If list is empty.
            if (deals == null || deals.Count == 0)
            {
                // default to $0.
                return new Deal(0, 0, string.Empty);
            }
            // Start at first deal.
            Deal cheapestDeal = deals[0];

            foreach (Deal deal in deals)
            {
                // Comapare each deal in list to see if its cheaper.
                if (deal.salePrice < cheapestDeal.salePrice)
                {
                    cheapestDeal = deal;
                }
            }
            return cheapestDeal;
        }
    }
}
