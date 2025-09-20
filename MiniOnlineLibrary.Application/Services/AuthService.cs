
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniOnlineLibrar.Infrastructure;
using MiniOnlineLibrary.Application.Interfaces;
using MiniOnlineLibrary.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MiniOnlineLibrary.Application.DTO.AuthDto;

namespace MiniOnlineLibrary.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly MiniLibraryDbContext _db;
        private readonly PasswordHasher<User> _hasher = new();
        private readonly IConfiguration _config;


        public AuthService(MiniLibraryDbContext db, IConfiguration config)
        {
            _db = db; _config = config;
        }


        public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already registered");


            var user = new User { Name = dto.Name, Email = dto.Email, Role = dto.Role ?? "User" };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            var token = GenerateJwt(user);
            return new AuthResultDto(token, user.UserId, user.Role, user.Name);
        }


        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new Exception("Invalid credentials");
            var r = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (r == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed) throw new Exception("Invalid credentials");
            var token = GenerateJwt(user);
            return new AuthResultDto(token, user.UserId, user.Role, user.Name);
        }


        private string GenerateJwt(User user)
        {
            var key = _config["Jwt:Key"] ?? "SuperSecretKeyForJwtDontUseInProd";
            var issuer = _config["Jwt:Issuer"] ?? "LibraryApi";
            var audience = _config["Jwt:Audience"] ?? "LibraryClient";


            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("name", user.Name)
                    };


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims,
            expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
