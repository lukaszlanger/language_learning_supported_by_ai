using System.Text.Json.Nodes;
using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Services
{
    public interface IOpenAIService
    {
        public Task<JsonObject> GenerateQuizAsync (string topic, string learningLanguage, int difficultyLevel);
    }
}