

using BusinessObject.DataAccess;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ProjectPrnContext _context;
        public AuthController(ProjectPrnContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginDto> SignIn(LoginDto user)
        {
            JObject json = JObject.Parse(System.IO.File.ReadAllText("appsettings.json"));
            var loggedUser = _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (loggedUser != null)
            {
                var issuer = (string)json["Security"]["Issuer"];
                var audience = (string)json["Security"]["Audience"];
                var key = Encoding.UTF8.GetBytes((string)json["Security"]["Key"]);

                var tokenDesciptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDesciptor);
                var stringToken = tokenHandler.WriteToken(token);

                return Ok(new LoginDto
                {
                    User = loggedUser,
                    Token = stringToken
                });
            }
            return Unauthorized();
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public ActionResult<User> SignUp(User user)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (existUser == null)
            {
                user.Role = 1;
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
            return Conflict("User existed");
        }

        [HttpGet("/{id}")]
        public ActionResult<User> GetDetail(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
