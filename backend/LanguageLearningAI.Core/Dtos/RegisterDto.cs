namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <example>Pa$$w0rd</example>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        /// <example>Jan</example>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        /// <example>Kowalski</example>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the native language of the user.
        /// </summary>
        /// <example>pl</example>
        public string NativeLanguage { get; set; }
    }
}