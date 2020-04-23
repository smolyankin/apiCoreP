using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace apiCoreP
{
    /// <summary>
    /// auth options
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// token issuer
        /// </summary>
        public const string ISSUER = "MyAuthServer";

        /// <summary>
        /// token audience
        /// </summary>
        public const string AUDIENCE = "MyAuthClient";

        const string KEY = "some_super_secret_key_123_!";

        /// <summary>
        /// token life time
        /// </summary>
        public const int LIFETIME = 86400; // 1 day

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
