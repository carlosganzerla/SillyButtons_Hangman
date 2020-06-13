using SillyButtons.Interfaces;
using System;
using System.Net.Http;

namespace SillyButtons.Game
{
    public class RandomWordApiGenerator : IWordGenerator
    {
        private readonly string apiUri;

        public RandomWordApiGenerator(string apiUri)
        {
            this.apiUri = apiUri;
        }


        public string GenerateWord()
        {
            using (var client = CreateHttpClient())
            {
                return GetRandomWordFromApi(client);
            }
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(apiUri)
            };
        }

        private string GetRandomWordFromApi(HttpClient client)
        {
            var apiJsonString = client.GetStringAsync(apiUri).GetAwaiter().GetResult();
            var actualWord = apiJsonString.Trim('"', '[', ']').ToUpper();
            return actualWord;
        }

    }
}
