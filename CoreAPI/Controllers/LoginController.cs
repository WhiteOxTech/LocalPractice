using CoreAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;



using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserModel loginUser)
        {
            IActionResult response = Unauthorized();

            var user = ValidateUserDetails(loginUser);
            if (user != null)
            {
                var tokenString = GenerateJsonWebJwtToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private string GenerateJsonWebJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.EmailAddress),
                new Claim("DateOfJoin",user.DateOfJoin.ToString("dd-MM-YYYY"))
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        private UserModel ValidateUserDetails(UserModel user)
        {
            UserModel loginUser = null;
            if (user != null)
            {
                if (user.UserName == "Naidu")
                    loginUser = new UserModel { UserName = user.UserName, EmailAddress = "Naidu.Loginuser@login.com" };


            }
            return loginUser;
        }
    }
}