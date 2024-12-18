using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class FlashcardService
    {
        private readonly FlashcardRepository _flashcardRepository;
        private readonly OpenAIService _openAIService;

        public FlashcardService(
            FlashcardRepository flashcardRepository,
            OpenAIService openAIService
            )
        {
            _flashcardRepository = flashcardRepository;
            _openAIService = openAIService;
        }

        public async Task<List<FlashcardDto>> GenerateAndSaveFlashcardsAsync(string topic, string learningLanguage, string nativeLanguage, int difficultyLevel, int lessonId)
        {
            var flashcards = await _openAIService.GenerateFlashcardsAsync(topic, learningLanguage, nativeLanguage, difficultyLevel);

            foreach (var flashcard in flashcards)
            {
                flashcard.LessonId = lessonId;
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
    }
}
