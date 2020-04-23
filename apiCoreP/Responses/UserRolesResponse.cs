using System.Collections.Generic;
using apiCoreP.Model;

namespace apiCoreP.Responses
{
    /// <summary>
    /// user roles response
    /// </summary>
    public class UserRolesResponse : BaseListResponse
    {
        /// <summary>
        /// items
        /// </summary>
        public List<UserRole> Items { get; set; } = new List<UserRole>();
    }
}
