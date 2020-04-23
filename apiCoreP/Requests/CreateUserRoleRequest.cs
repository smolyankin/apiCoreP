using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apiCoreP.Enums;

namespace apiCoreP.Requests
{
    /// <summary>
    /// new user role
    /// </summary>
    public class CreateUserRoleRequest
    {
        /// <summary>
        /// role name
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// role types
        /// </summary>
        public List<UserRoleType> UserRoleTypes { get; set; } = new List<UserRoleType>();

        /// <summary>
        /// user ids with this role
        /// </summary>
        public virtual List<long> UserIds { get; set; } = new List<long>();
    }
}
