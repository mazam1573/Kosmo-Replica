using EduConsultant.Data;
using EduConsultant.DTOs;
using EduConsultant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduConsultant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly EduConsultantContext _context;
        public AuthController(EduConsultantContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto data)
        {
            if(!_context.User.Any(x => x.Email == data.Email)) 
                return NotFound($"User with email {data.Email} doesn't exist!");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == data.Email);
            if (user.Password != data.Password)
                return NotFound($"Password for email {data.Email} is incorrect!");

            if (user.Password == data.Password)
            {
                var token = GenerateJwtToken(user);
                var newUser = new ReadLongUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    First_Name = user.First_Name,
                    Last_Name = user.Last_Name,
                    Phone = user.Phone,
                    Address = user.Address,
                    City = user.City,
                    Zipcode = user.Zipcode,
                    Status = user.Status,
                    Country = user.Country,
                    Token = token,
                };
                return Ok(newUser);
            }

            return NoContent();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("edu-consultant-your-very-strong-secret-key-123"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "edu-consultant",
                audience: "edu-consultant",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
