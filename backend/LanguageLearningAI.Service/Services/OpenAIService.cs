using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Service.Services
{
    public class OpenAIService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _model;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";
        private readonly HttpClient _httpClient;

        public OpenAIService(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("API Key not found in configuration");
            _model = configuration["OpenAI:Model"] ?? throw new ArgumentNullException("Model not specified in configuration");
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GenerateQuizAsync(string topic, string learningLanguage, int difficultyLevel)
        {
            var difficultyName = Enum.GetName(typeof(DifficultyLevel), difficultyLevel) 
                                 ?? throw new ArgumentOutOfRangeException(nameof(difficultyLevel), "Invalid difficulty level");

            var prompt = $@"
            Generate a JSON object with 10 quiz questions on the topic '{topic}' in '{learningLanguage}' language 
            for '{difficultyName}' difficulty level. The questions should teach and practice new vocabulary in the context of learning a foreign language.
            Each question should have the following structure:
            {{
                ""question"": ""string"",
                ""answers"": [""string"", ""string"", ""string"", ""string""],
                ""correct_answer"": ""string""
            }}";

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = "You are an assistant generating quiz questions." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 1000,
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
