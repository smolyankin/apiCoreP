using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiCoreP.Services;
using apiCoreP.Requests;

namespace apiCoreP.Controllers
{
    /// <summary>
    /// auth controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// make token for user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromForm]AuthRequest request)
        {
            var identity = await _authService.GetIdentity(request.Email, request.Password);

            if (identity == null)
                return BadRequest(new { errorText = "invalid email or password" });

            var response = _authService.IdentityResponse(identity);

            return new JsonResult(new
            {
                access_token = response.Item1,
                username = response.Item2
            });
        }
    }
}
