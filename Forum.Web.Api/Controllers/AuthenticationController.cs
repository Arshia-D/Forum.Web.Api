using Forum.Application.Dto;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly UserService _userService;


        public AuthenticationController(
            AuthenticationService authenticationService,
            UserService userService)  // Constructor injection of UserService
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(
            [FromBody] AuthenticateDto model)
        {
            var result = await _authenticationService.AuthenticateAsync(
                model.Username, 
                model.Password);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.User);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto model)
        {
            var (Id, Error) = await _userService.CreateUserAsync(model);

            if (!string.IsNullOrEmpty(Error))
            {
                return BadRequest(Error);
            }

            return Ok(new { UserId = Id });
        }

    }
}
