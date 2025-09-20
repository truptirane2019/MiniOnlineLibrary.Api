using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.DTO
{
    public class AuthDto
    {
        public record RegisterDto(string Name, string Email, string Password, string? Role);
        public record LoginDto(string Email, string Password);
        public record AuthResultDto(string Token, int UserId, string Role, string Name);
    }
}
