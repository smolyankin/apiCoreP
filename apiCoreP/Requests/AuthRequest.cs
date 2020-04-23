namespace apiCoreP.Requests
{
    /// <summary>
    /// login request
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// password (hash)
        /// </summary>
        public string Password { get; set; }
    }
}
