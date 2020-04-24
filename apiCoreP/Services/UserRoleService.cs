using System.Linq;
using System.Threading.Tasks;
using apiCoreP.Data;
using apiCoreP.Extensions;
using apiCoreP.Model;
using apiCoreP.Requests;
using apiCoreP.Responses;
using Microsoft.EntityFrameworkCore;

namespace apiCoreP.Services
{
    /// <summary>
    /// user role service
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        private readonly ApplicationContext _context;
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        public UserRoleService(ApplicationContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// get user role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserRole> GetById(long id)
        {
            return await _context.UserRoles.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// get user role details by id
        /// </summary>
        /// <param name="id">user role id</param>
        /// <returns></returns>
        public async Task<UserRoleResponse> GetDetailsById(long id)
        {
            var userRole = await GetById(id);
            if (userRole == null)
                return null;

            var result = userRole.Transform<UserRoleResponse>();

            var users = await _userService.GetUsersWithoutRole();
            var availableUsers = result.Users.Union(users).ToList();
            if (availableUsers.Any())
                result.AvailableUsers = availableUsers.Select(x => x.Transform<UserResponse>()).ToList();

            return result;
        }

        /// <summary>
        /// create user role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserRole> Create(CreateUserRoleRequest request)
        {
            var userRole = request.Transform<UserRole>();

            _context.UserRoles.Add(userRole);

            if (request.UserIds != null && request.UserIds.Any())
            {
                var users = await _userService.GetUsersByIds(request.UserIds);
                userRole.Users = users;
            }

            await _context.SaveChangesAsync();

            return userRole;
        }

        /// <summary>
        /// update user role
        /// </summary>
        /// <param name="request">user role request</param>
        /// <returns></returns>
        public async Task<UserRole> Edit(EditUserRoleRequest request)
        {
            var userRole = await GetById(request.Id);
            if (userRole == null)
                return null;

            request.ToCopy(userRole);
                
            var users = await _userService.GetUsersByIds(request.UserIds);
            userRole.Users.RemoveAll(x => !request.UserIds.Contains(x.Id));
            userRole.Users.AddRange(users.Where(x => !userRole.Users.Contains(x)));

            await _context.SaveChangesAsync();

            return userRole;
        }

        /// <summary>
        /// get user roles by page
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<UserRolesResponse> GetUserRoles(int? skip = 0, int? count = 10)
        {
            var result = new UserRolesResponse();
            
            count.LimitCount();
            
            result.Count = await _context.UserRoles.CountAsync();
            result.Items = await _context.UserRoles.Include(x => x.Users).OrderBy(x => x.Title).Skip(skip ?? 0).Take((int)count).ToListAsync();

            return result;
        }
    }

    /// <summary>
    /// user role interface
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// get user role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserRole> GetById(long id);

        /// <summary>
        /// get user role details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserRoleResponse> GetDetailsById(long id);

        /// <summary>
        /// create user role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRole> Create(CreateUserRoleRequest request);

        /// <summary>
        /// update user role
        /// </summary>
        /// <param name="request">user role request</param>
        /// <returns></returns>
        Task<UserRole> Edit(EditUserRoleRequest request);

        /// <summary>
        /// get user roles by page
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<UserRolesResponse> GetUserRoles(int? skip = 0, int? count = 10);
    }
}
