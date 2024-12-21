using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class FlashcardService
    {
        private readonly FlashcardRepository _flashcardRepository;
        private readonly OpenAIService _openAIService;
        private readonly LessonService _lessonService;
        private readonly AuthService _authService;

        public FlashcardService(
            FlashcardRepository flashcardRepository,
            OpenAIService openAIService,
            LessonService lessonService,
            AuthService authService)
        {
            _flashcardRepository = flashcardRepository;
            _openAIService = openAIService;
            _lessonService = lessonService;
            _authService = authService;
        }

        public async Task<FlashcardDto> CreateAsync(
            string nativeLanguage,
            string learningLanguage,
            string lessonTopic,
            int difficultyLevel,
            int lessonId,
            string term,
            string? translation,
            string? details,
            string? usage)
        {
            var aiResponseData = await _openAIService.GenerateFlashcardDetailsAsync(learningLanguage, nativeLanguage, lessonTopic, difficultyLevel, term);

            var flashcardCreate = new FlashcardCreateDto
            {
                Term = term,
                LessonId = lessonId,
                Details = aiResponseData.ContainsKey("details") && !string.IsNullOrEmpty(aiResponseData["details"])
                    ? aiResponseData["details"]
                    : details,
                Translation = aiResponseData.ContainsKey("translation") && !string.IsNullOrEmpty(aiResponseData["translation"])
                    ? aiResponseData["translation"]
                    : translation,
                Usage = aiResponseData.ContainsKey("usage") && !string.IsNullOrEmpty(aiResponseData["usage"])
                    ? aiResponseData["usage"]
                    : usage,
            };

            var flashcardId = await _flashcardRepository.AddAsync(EntityMapper.Map(flashcardCreate));

            return await GetFlashcardByIdAsync(flashcardId);
        }

        public async Task<List<FlashcardDto>> GenerateAndSaveFlashcardsAsync(FlashcardGenerateWithAIDto flashcardGenerateWithAiDto)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(flashcardGenerateWithAiDto.LessonId) ?? throw new ArgumentException($"Lesson with ID {flashcardGenerateWithAiDto.LessonId} not found.");

            var user = await _authService.GetUserByIdAsync(flashcardGenerateWithAiDto.UserId) ?? throw new ArgumentException($"User with ID {flashcardGenerateWithAiDto.UserId} not found.");

            var flashcards = await _openAIService.GenerateFlashcardsAsync(
                lesson.Topic,
                lesson.LearningLanguage,
                user.NativeLanguage,
                lesson.DifficultyLevel
            );

            foreach (var flashcard in flashcards)
            {
                flashcard.LessonId = flashcardGenerateWithAiDto.LessonId;
                var flashcardId = await _flashcardRepository.AddAsync(EntityMapper.Map(flashcard));
                flashcard.Id = flashcardId;
            }

            return flashcards;
        }

        public async Task<IEnumerable<FlashcardDto>> GetAllFlashcardsAsync()
        {
            var flashcards = await _flashcardRepository.GetAllAsync();
            return flashcards.Select(EntityMapper.Map);
        }

        public async Task<IEnumerable<FlashcardDto>> GetFlashcardsByLessonIdAsync(int id)
        {
            var flashcards = await _flashcardRepository.GetAllByLessonIdAsync(id);
            return flashcards.Select(EntityMapper.Map);
        }

        public async Task<FlashcardDto> GetFlashcardByIdAsync(int id)
        {
            return EntityMapper.Map(await _flashcardRepository.GetByIdAsync(id));
        }
    }
}
