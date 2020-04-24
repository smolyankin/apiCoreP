using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apiCoreP.Requests;
using apiCoreP.Services;
using apiCoreP.Responses;

namespace apiCoreP.Controllers
{
    /// <summary>
    /// user role controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRoleService"></param>
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// get user roles with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<UserRolesResponse>> UserRoles(int? skip = 0, int? count = 10)
        {
            return await _userRoleService.GetUserRoles(skip, count);
        }

        /// <summary>
        /// get user role details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleResponse>> GetDetailsById(int id)
        {
            var userRole = await _userRoleService.GetDetailsById(id);
            if (userRole == null)
                return NotFound();

            return userRole;
        }

        /// <summary>
        /// create user role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRoleRequest request)
        {
            var userRole = await _userRoleService.Create(request);

            return Created(HttpContext.Request.Path, userRole);
        }

        /// <summary>
        /// update user role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(EditUserRoleRequest request)
        {
            var userRole = await _userRoleService.Edit(request);
            if (userRole == null)
                return NotFound();

            return Ok();
        }
    }
}
