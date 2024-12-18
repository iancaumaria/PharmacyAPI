using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using PharmacyAPI.Data;
using PharmacyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;


namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly PharmacyDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(PharmacyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            // Debugging temporar
            Console.WriteLine($"Input Password: {model.Password}");
            Console.WriteLine($"Stored Hash: {user?.PasswordHash}");
            Console.WriteLine($"Verify Result: {BCrypt.Net.BCrypt.Verify(model.Password, user?.PasswordHash)}");

            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }


        [HttpGet("hash-password")]
        public IActionResult GeneratePasswordHash([FromQuery] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password cannot be empty.");
            }

            // Generare hash folosind BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return Ok(new { PasswordHash = passwordHash });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Adaugă logica pentru verificarea parolei (hashing)
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
