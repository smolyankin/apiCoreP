using Microsoft.Extensions.DependencyInjection;
using apiCoreP.Services;

namespace apiCoreP.DI
{
    /// <summary>
    /// di for services
    /// </summary>
    public static class ServicesDI
    {
        /// <summary>
        /// load services di
        /// </summary>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<ISubscriberService, SubscriberService>();
        }
    }
}
