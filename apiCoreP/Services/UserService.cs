using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiCoreP.Data;
using apiCoreP.Enums;
using apiCoreP.Extensions;
using apiCoreP.Model;
using apiCoreP.Requests;
using apiCoreP.Responses;
using Microsoft.EntityFrameworkCore;

namespace apiCoreP.Services
{
    /// <summary>
    /// user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get user by identity
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdentity(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        /// <summary>
        /// get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetUserByEmail(string email)
        {
            var user = await _context.Users.Include(x => x.UserRole).FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;
            
            var response = user?.Transform<UserResponse>();
            response.UserRoleTypes = user.UserRole?.UserRoleTypes ?? new List<UserRoleType>();

            return response;
        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task RegisterUser(CreateUserRequest request)
        {
            var user = request.Transform<User>();

            user.UserType = UserType.Default;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get users by id list
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersByIds(List<long> userIds)
        {
            return await _context.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
        }

        /// <summary>
        /// get users without role
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersWithoutRole()
        {
            return await _context.Users.Where(x => !x.UserRoleId.HasValue).ToListAsync();
        }

        /// <summary>
        /// check user current role has permission to role type
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="types">role types</param>
        /// <returns></returns>
        public bool CheckAccess(string email, UserRoleType[] types)
        {
            return _context.Users
                .Include(x => x.UserRole)
                .FirstOrDefault(x => x.Email == email)?.UserRole?.UserRoleTypes.Any(x => types.Contains(x))
                ?? false;
        }
    }

    /// <summary>
    /// user interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// get user by identity
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> GetUserByIdentity(string email, string password);
        
        /// <summary>
        /// get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserResponse> GetUserByEmail(string email);

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserById(long id);

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task RegisterUser(CreateUserRequest request);

        /// <summary>
        /// get users by id list
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<User>> GetUsersByIds(List<long> userIds);

        /// <summary>
        /// get users without role
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsersWithoutRole();

        /// <summary>
        /// check user current role has permission to role type
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="types">role types</param>
        /// <returns></returns>
        bool CheckAccess(string email, UserRoleType[] types);
    }
}
