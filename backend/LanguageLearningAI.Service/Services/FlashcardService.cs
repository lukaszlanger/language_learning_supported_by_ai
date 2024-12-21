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

        public async Task<FlashcardDto> CreateAsync(
            string learningLanguage,
            string nativeLanguage,
            string lessonTopic,
            int difficultyLevel,
            string term,
            int lessonId)
        {
            var aiResponseData = await _openAIService.GenerateFlashcardDetailsAsync(learningLanguage, nativeLanguage, lessonTopic, difficultyLevel,
                term);

            var flashcardCreate = new FlashcardCreateDto() { Term = term, LessonId = lessonId };
            foreach (var item in aiResponseData)
            {
                flashcardCreate.Details = item.ContainsKey("details") ? item["details"] : string.Empty;
                flashcardCreate.Translation = item.ContainsKey("translation") ? item["translation"] : string.Empty;
                flashcardCreate.Usage = item.ContainsKey("usage") ? item["usage"] : string.Empty;
            }

            var flashcardId = await _flashcardRepository.AddAsync(EntityMapper.Map(flashcardCreate));
            return await GetFlashcardByIdAsync(flashcardId);
        }

        public async Task<List<FlashcardDto>> GenerateAndSaveFlashcardsAsync(FlashcardGenerateWithAIDto flashcardGenerateWithAiDto)
        {
            var flashcards = await _openAIService.GenerateFlashcardsAsync(flashcardGenerateWithAiDto.Topic, flashcardGenerateWithAiDto.LearningLanguage, flashcardGenerateWithAiDto.NativeLanguage, flashcardGenerateWithAiDto.DifficultyLevel);

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
