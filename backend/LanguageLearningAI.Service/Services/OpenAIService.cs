using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Service.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;
        private readonly string _model;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";
        private readonly HttpClient _httpClient;

        public OpenAIService(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("API Key not found in configuration");
            _model = configuration["OpenAI:Model"] ?? throw new ArgumentNullException("Model not specified in configuration");
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }


        public async Task<List<QuizQuestionDto>> GenerateQuizQuestionsAsync(string topic, string learningLanguage, int difficultyLevel)
        {
            var prompt = GeneratePromptForQuizQuestions(topic, learningLanguage, difficultyLevel);

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

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"OpenAI API request failed with status {response.StatusCode}: {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

            return DeserializeResponse(responseString);
        }

        private string GeneratePromptForQuizQuestions(string topic, string learningLanguage, int difficultyLevel)
        {
            var difficultyName = Enum.GetName(typeof(DifficultyLevel), difficultyLevel)
                                 ?? throw new ArgumentOutOfRangeException(nameof(difficultyLevel), "Invalid difficulty level");

            return $@"
            Generate a JSON object with 10 quiz questions on the topic '{topic}' in '{learningLanguage}' language 
            for '{difficultyName}' difficulty level. The questions should teach and practice new vocabulary in the context of learning a foreign language.
            Each question should have the following structure:
            {{
                ""question"": ""string"",
                ""answers"": [""string"", ""string"", ""string"", ""string""],
                ""correctAnswer"": ""string""
            }}";
        }

        private string GeneratePromptForFlashcards(string topic, string learningLanguage, int difficultyLevel)
        {
            var difficultyName = Enum.GetName(typeof(DifficultyLevel), difficultyLevel)
                                 ?? throw new ArgumentOutOfRangeException(nameof(difficultyLevel), "Invalid difficulty level");

            return $@"
            Generate a JSON object with 10 quiz questions on the topic '{topic}' in '{learningLanguage}' language 
            for '{difficultyName}' difficulty level. The questions should teach and practice new vocabulary in the context of learning a foreign language.
            Each question should have the following structure:
            {{
                ""question"": ""string"",
                ""answers"": [""string"", ""string"", ""string"", ""string""],
                ""correctAnswer"": ""string""
            }}";
        }

        private List<QuizQuestionDto> DeserializeResponse(string responseString)
        {
            try
            {
                var root = JsonDocument.Parse(responseString).RootElement;

                var content = root
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (content.StartsWith("```json"))
                {
                    content = content.Replace("```json", "").Replace("```", "").Trim();
                }

                Console.WriteLine($"Cleaned content: {content}");

                var deserialized = JsonSerializer.Deserialize<List<QuizQuestionDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserialized == null || deserialized.Count == 0)
                {
                    throw new JsonException("Deserialized content is empty or null.");
                }

                return deserialized;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing OpenAI response: {ex.Message}");
                throw new InvalidOperationException("Error processing OpenAI response", ex);
            }
        }
    }
}
