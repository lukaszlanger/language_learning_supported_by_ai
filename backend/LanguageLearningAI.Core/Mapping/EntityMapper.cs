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

        public static Quiz Map(QuizDto dto) =>
            new()
            {
                Id = dto.Id,
                LessonId = dto.LessonId,
                Questions = dto.Questions.Select(Map).ToList()
            };

        public static QuizQuestion Map(QuizQuestionDto dto) =>
            new()
            {
                Id = dto.Id,
                QuizId = dto.QuizId,
                Question = dto.Question,
                Answers = dto.Answers.ToList(),
                CorrectAnswer = dto.CorrectAnswer,
                UserAnswer = dto.UserAnswer,
                IsCorrect = dto.IsCorrect
            };

        public static QuizDto Map(Quiz entity) =>
            new()
            {
                Id = entity.Id,
                LessonId = entity.LessonId,
                Questions = entity.Questions.Select(Map).ToList()
            };

        public static QuizQuestionDto Map(QuizQuestion entity) =>
            new()
            {
                Id = entity.Id,
                QuizId = entity.QuizId,
                Question = entity.Question,
                Answers = entity.Answers.ToList(),
                CorrectAnswer = entity.CorrectAnswer,
                UserAnswer = entity.UserAnswer,
                IsCorrect = entity.IsCorrect
            };

    }
}
