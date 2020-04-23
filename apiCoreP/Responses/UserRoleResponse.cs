using System.Collections.Generic;
using apiCoreP.Model;

namespace apiCoreP.Responses
{
    /// <summary>
    /// user role details response
    /// </summary>
    public class UserRoleResponse : UserRole
    {
        /// <summary>
        /// users without role
        /// </summary>
        public List<UserResponse> AvailableUsers { get; set; } = new List<UserResponse>();
    }
}
