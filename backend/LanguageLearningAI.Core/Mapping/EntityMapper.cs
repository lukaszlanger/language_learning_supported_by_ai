using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Core.Mapping
{
    public static class EntityMapper
    {
        public static FlashcardDto Map(Flashcard flashcard) =>
            new()
            {
                Id = flashcard.Id,
                Term = flashcard.Term,
                Details = flashcard.Details,
                Translation = flashcard.Translation,
                Usage = flashcard.Usage,
                LessonId = flashcard.LessonId
            };

        public static Flashcard Map(FlashcardDto dto) =>
            new()
            {
                Id = dto.Id,
                Term = dto.Term,
                Details = dto.Details,
                Translation = dto.Translation,
                Usage = dto.Usage,
                LessonId = dto.LessonId
            };

        public static Flashcard Map(FlashcardCreateDto dto) =>
            new()
            {
                Term = dto.Term,
                Details = dto.Details,
                Translation = dto.Translation,
                Usage = dto.Usage,
                LessonId = dto.LessonId
            };

        public static LessonDto Map(Lesson lesson) =>
            new()
            {
                Id = lesson.Id,
                Topic = lesson.Topic,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage,
                UserId = lesson.UserId
            };

        public static Lesson Map(LessonDto dto) =>
            new()
            {
                Id = dto.Id,
                Topic = dto.Topic,
                DifficultyLevel = (DifficultyLevel)dto.DifficultyLevel,
                LearningLanguage = dto.LearningLanguage,
                UserId = dto.UserId
            };

        public static Lesson Map(LessonCreateDto dto) =>
            new()
            {
                Topic = dto.Topic,
                DifficultyLevel = (DifficultyLevel)dto.DifficultyLevel,
                LearningLanguage = dto.LearningLanguage,
                UserId = dto.UserId
            };
    }
}
