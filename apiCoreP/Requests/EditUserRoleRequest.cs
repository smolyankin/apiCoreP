using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apiCoreP.Enums;

namespace apiCoreP.Requests
{
    /// <summary>
    /// update user role request
    /// </summary>
    public class EditUserRoleRequest
    {
        /// <summary>
        /// name
        /// </summary>
        [Required]
        public long Id { get; set; }

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
        /// new users for role
        /// </summary>
        public List<long> UserIds { get; set; } = new List<long>();
    }
}
