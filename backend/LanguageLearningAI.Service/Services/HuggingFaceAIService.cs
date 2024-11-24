using System.Text;
using System.Text.Json;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Core.Repositories;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Service.Services
{
    public class HuggingFaceAIService : IAIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILessonRepository _lessonRepository;
        private readonly IPhraseRepository _phraseRepository;
        private readonly IQuizRepository _quizRepository;

        private const string HuggingFaceUrl = "https://api-inference.huggingface.co/models/your-model-name";

        public HuggingFaceAIService(
            IHttpClientFactory httpClientFactory,
            ILessonRepository lessonRepository,
            IPhraseRepository phraseRepository,
            IQuizRepository quizRepository)
        {
            _httpClientFactory = httpClientFactory;
            _lessonRepository = lessonRepository;
            _phraseRepository = phraseRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Lesson> GenerateLessonAsync(string topic, string language, int difficultyLevel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var payload = new
            {
                topic,
                language,
                difficulty = difficultyLevel
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your-huggingface-api-key");

            var response = await httpClient.PostAsync(HuggingFaceUrl, content);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var generatedPhrases = JsonSerializer.Deserialize<List<string>>(responseData);

            var lesson = new Lesson
            {
                Topic = topic,
                DifficultyLevel = (DifficultyLevel)difficultyLevel,
                LearningLanguage = language,
                Phrases = generatedPhrases.Select(p => new Phrase { Text = p, Translation = "" }).ToList()
            };

            await _lessonRepository.AddAsync(lesson);

            return lesson;
        }

        public async Task<IEnumerable<Quiz>> GenerateQuizAsync(int lessonId, int difficultyLevel)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);

            if (lesson == null)
                throw new ArgumentException($"Lesson with ID {lessonId} not found.");

            var quizzes = new List<Quiz>();

            foreach (var phrase in lesson.Phrases)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var payload = new
                {
                    phrase = phrase.Text,
                    difficulty = difficultyLevel
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your-huggingface-api-key");

                var response = await httpClient.PostAsync(HuggingFaceUrl, content);

                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var quizData = JsonSerializer.Deserialize<Quiz>(responseData);

                // quizData.Phrase.LessonId = lessonId;
                quizData.PhraseId = phrase.Id;

                quizzes.Add(quizData);
            }

            // await _quizRepository.AddRangeAsync(quizzes);
            return quizzes;
        }
    }
}
