using Google.GenAI;
using System.Collections.Generic;

namespace BankingPortal.API.Services
{
    public class GeminiService
    {
        private readonly IConfiguration _configuration;

        public GeminiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> AskGeminiAsync(string question)
        {
            var apiKey = _configuration["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("Gemini API key is missing.");
            }

            var client = new Client(apiKey: apiKey);

            var response = await client.Models.GenerateContentAsync(
                model: "gemini-3-flash-preview",
                contents: question
            );

            if (response == null)
            {
                throw new Exception("Gemini returned a null response.");
            }

            return response.Text ?? "Gemini returned no text.";
        }
    }
}