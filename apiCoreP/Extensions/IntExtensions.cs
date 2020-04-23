namespace apiCoreP.Extensions
{
    /// <summary>
    /// extentions for int
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// max 100 elements
        /// </summary>
        /// <param name="count"></param>
        public static void LimitCount(this ref int? count)
        {
            count = count > 100 ? 100 : count ?? 10;
        }
    }
}
