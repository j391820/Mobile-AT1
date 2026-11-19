using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace TestAssistant1
{
    public class IGDBApiService
    {

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

    }
}
