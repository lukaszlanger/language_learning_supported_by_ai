using Microsoft.AspNetCore.Identity;

namespace LanguageLearningAI.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NativeLanguage { get; set; }
        public string LearningLanguage { get; set; }
        public string DifficultyLevel { get; set; }
    }
}
