namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the login data transfer object.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <example>password123</example>
        public string Password { get; set; }
    }
}
