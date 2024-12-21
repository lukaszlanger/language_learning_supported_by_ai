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
            var prompt = GetPromptForQuizQuestions(topic, learningLanguage, difficultyLevel);
            return await SendRequestAsync<List<QuizQuestionDto>>(prompt, "You are an assistant generating quiz questions.");
        }

        public async Task<List<FlashcardDto>> GenerateFlashcardsAsync(string topic, string learningLanguage, string nativeLanguage, int difficultyLevel)
        {
            var prompt = GetPromptForFlashcards(topic, learningLanguage, nativeLanguage, difficultyLevel);
            return await SendRequestAsync<List<FlashcardDto>>(prompt, "You are an assistant generating flashcard phrases or words.");
        }

        public async Task<Dictionary<string, string>> GenerateFlashcardDetailsAsync(
            string learningLanguage,
            string nativeLanguage,
            string lessonTopic,
            int difficultyLevel,
            string term)
        {
            var prompt = GetPromptForFlashcardDetails(learningLanguage, nativeLanguage, lessonTopic, difficultyLevel, term);
            return await SendRequestAsync<Dictionary<string, string>>(prompt, "You are an assistant generating short details (description) for a flashcard term.");
        }

        private async Task<T> SendRequestAsync<T>(string prompt, string systemMessage)
        {
            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = systemMessage },
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
            Console.WriteLine($"Raw API Response: {responseString}");

            return DeserializeResponse<T>(responseString);
        }

        private T DeserializeResponse<T>(string responseString)
        {
            try
            {
                var root = JsonDocument.Parse(responseString).RootElement;

                var content = root
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (!string.IsNullOrWhiteSpace(content) && content.StartsWith("```json"))
                {
                    content = content.Replace("```json", "").Replace("```", "").Trim();
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new JsonException("Content is empty after cleaning.");
                }

                var deserialized = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserialized == null)
                {
                    throw new JsonException("Deserialized content is null.");
                }

                return deserialized;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Parsing Error: {jsonEx.Message}");
                throw new InvalidOperationException("Error processing OpenAI response: JSON Parsing failed.", jsonEx);
            }
        }

        private string GetPromptForQuizQuestions(string topic, string learningLanguage, int difficultyLevel)
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

        private string GetPromptForFlashcards(string topic, string learningLanguage, string nativeLanguage, int difficultyLevel)
        {
            var difficultyName = Enum.GetName(typeof(DifficultyLevel), difficultyLevel)
                                 ?? throw new ArgumentOutOfRangeException(nameof(difficultyLevel), "Invalid difficulty level");

            return $@"
            Generate a JSON object with about 15 unique flashcards on the topic '{topic}' in '{learningLanguage}' language
            for '{difficultyName}' difficulty level. The flashcards should teach and practice new vocabulary in the context of learning a foreign language.
            Each flashcard should have the following structure:
            {{
                ""term"": ""string"",
                ""details"": ""string"",
                ""translation"": ""string"",
                ""usage"": ""string""
            }}";
        }

        private string GetPromptForFlashcardDetails(string learningLanguage, string nativeLanguage, string lessonTopic, int difficultyLevel, string term)
        {
            var difficultyName = Enum.GetName(typeof(DifficultyLevel), difficultyLevel)
                                 ?? throw new ArgumentOutOfRangeException(nameof(difficultyLevel), "Invalid difficulty level");

            return $@"
            Generate a JSON object with the following fields:
            {{
                ""details"": ""string"",
                ""translation"": ""string"",
                ""usage"": ""string""
            }}, where details is a brief description or explanation for the term '{term}' in the '{learningLanguage}' language,
            translation is the translation to '{nativeLanguage}' language of the term '{term}',
            and usage is an example usage of the term '{term}' in the '{learningLanguage}' language, suitable for the '{difficultyName}' level, and in the context of the lesson topic '{lessonTopic}'";
        }
    }
}
