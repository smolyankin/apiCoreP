using System.Collections.Generic;
using apiCoreP.Model;

namespace apiCoreP.Responses
{
    /// <summary>
    /// subscribers response
    /// </summary>
    public class SubscribersResponse : BaseListResponse
    {
        /// <summary>
        /// items
        /// </summary>
        public List<Subscriber> Items { get; set; } = new List<Subscriber>();
    }
}
