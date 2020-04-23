using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apiCoreP.Enums;

namespace apiCoreP.Model
{
    /// <summary>
    /// user role
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// role name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// role types
        /// </summary>
        public List<UserRoleType> UserRoleTypes { get; set; } = new List<UserRoleType>();

        /// <summary>
        /// users with this role
        /// </summary>
        public virtual List<User> Users { get; set; }
    }
}
