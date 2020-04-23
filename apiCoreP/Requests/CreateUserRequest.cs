using System.ComponentModel.DataAnnotations;

namespace apiCoreP.Requests
{
    /// <summary>
    /// new user (register)
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// email (login)
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// password (hash)
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
