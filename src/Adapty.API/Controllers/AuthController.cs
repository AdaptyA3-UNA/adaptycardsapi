using Microsoft.AspNetCore.Mvc;
using Adapty.API.Models;
using Adapty.API.DTOs;
using Adapty.API.Services;
using Microsoft.AspNetCore.Identity; // Importante

namespace Adapty.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly AuthService _authService;

        public AuthController(UserManager<Users> userManager, AuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            var user = new Users
            {
                FullName = request.Name,
                Email = request.Email,
                UserName = request.Email,
                PasswordHash = request.Password
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            Console.WriteLine($"Senha enviada: '{request.Password}'");
            Console.WriteLine($"Senha salva:   '{user.PasswordHash}'");

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new { message = "E-mail ou senha inválidos." });
            }
        var token = _authService.GenerateJwtToken(user);

        return Ok(new AuthResponseDto(token));
        }
    }
}