using System.ComponentModel.DataAnnotations;
using apiCoreP.Enums;

namespace apiCoreP.Model
{
    /// <summary>
    /// user
    /// </summary>
    public class User
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long Id { get; set; }
        
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

        /// <summary>
        /// user role
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// id user role
        /// </summary>
        public virtual long? UserRoleId { get; set; }

        /// <summary>
        /// user role entity
        /// </summary>
        public UserRole UserRole { get; set; }
    }
}
