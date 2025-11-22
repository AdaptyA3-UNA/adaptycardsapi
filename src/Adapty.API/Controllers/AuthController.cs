using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adapty.API.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace Adapty.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            // TODO: Lógica de criar usuário no banco
            return Ok(new { message = "Usuário registrado com sucesso" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserProfileDto request)
        {
            // TODO: Validar usuário e gerar JWT
            return Ok(new { token = "exemplo_token_jwt_123456" });
        }
    }
}