using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Core.Mapping
{
    public static class EntityMapper
    {
        public static PhraseDto Map(Phrase phrase) =>
            new PhraseDto
            {
                Id = phrase.Id,
                Text = phrase.Text,
                Translation = phrase.Translation
            };

        public static Phrase Map(PhraseDto dto) =>
            new Phrase
            {
                Id = dto.Id,
                Text = dto.Text,
                Translation = dto.Translation
            };

        public static QuizDto Map(Quiz quiz) =>
            new QuizDto
            {
                Id = quiz.Id,
                PhraseId = quiz.PhraseId,
                QuestionText = quiz.QuestionText,
                CorrectAnswer = quiz.CorrectAnswer,
                UserAnswer = quiz.UserAnswer,
                IsCorrect = quiz.IsCorrect,
                Attempts = quiz.Attempts
            };

        public static Quiz Map(QuizDto dto) =>
            new Quiz
            {
                Id = dto.Id,
                PhraseId = dto.PhraseId,
                QuestionText = dto.QuestionText,
                CorrectAnswer = dto.CorrectAnswer,
                UserAnswer = dto.UserAnswer,
                IsCorrect = dto.IsCorrect,
                Attempts = dto.Attempts
            };

        public static LessonDto Map(Lesson lesson) =>
            new LessonDto
            {
                Id = lesson.Id,
                Topic = lesson.Topic,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage,
                UserId = lesson.UserId
            };

        public static Lesson Map(LessonDto dto) =>
            new Lesson
            {
                Id = dto.Id,
                Topic = dto.Topic,
                DifficultyLevel = (DifficultyLevel)dto.DifficultyLevel,
                LearningLanguage = dto.LearningLanguage,
                UserId = dto.UserId
            };
    }
}
