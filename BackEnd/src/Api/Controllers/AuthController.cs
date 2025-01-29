using System.Threading.Tasks;
using Application.Request.Auth;
using Application.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register(RegisterUserRequest registerUser)
        {
            var token = await _authService.Register(registerUser);

            if (string.IsNullOrWhiteSpace(token))
                return Problem("Falha ao registrar o usuário");

            return Created(nameof(Register), token);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(LoginUserRequest loginUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var token = await _authService.Login(loginUser);

            if (string.IsNullOrWhiteSpace(token))
                return Problem("Usuário ou senha incorretos");

            return Ok(token);
        }
    }
}