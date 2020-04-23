using System.Collections.Generic;
using apiCoreP.Enums;

namespace apiCoreP.Responses
{
    /// <summary>
    /// user short response
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// email (login)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// user role
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<UserRoleType> UserRoleTypes { get; set; } = new List<UserRoleType>();
    }
}
