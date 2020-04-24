using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apiCoreP.Model;
using apiCoreP.Requests;
using apiCoreP.Services;
using apiCoreP.Responses;

namespace apiCoreP.Controllers
{
    /// <summary>
    /// user controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersService"></param>
        public UserController(IUserService usersService)
        {
            _userService = usersService;
        }

        /// <summary>
        /// get current user info
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        public async Task<ActionResult<UserResponse>> Me()
        {
            var user = await _userService.GetUserByEmail(User.Identity.Name);
            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            var exist = await _userService.GetUserByEmail(request.Email);
            if (exist != null)
                return BadRequest("user already exist"); 
            
            await _userService.RegisterUser(request);

            return Ok();
        }

        /// <summary>
        /// get users without role
        /// </summary>
        /// <returns></returns>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<User>>> UsersWithoutRole()
        {
            return await _userService.GetUsersWithoutRole();
        }
    }
}
