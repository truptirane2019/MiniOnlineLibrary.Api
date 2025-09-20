using Microsoft.AspNetCore.Mvc;
using MiniOnlineLibrary.Application.Interfaces;
using MiniOnlineLibrary.Application.Services;
using static MiniOnlineLibrary.Application.DTO.AuthDto;

namespace MiniOnlineLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try { var res = await _auth.RegisterAsync(dto); return Ok(res); }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var res = await _auth.LoginAsync(dto);
            if (res is null) return Unauthorized();
            return Ok(res);
        }
    }
}
