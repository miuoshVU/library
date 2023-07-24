using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library.API.Controllers
{
    [Route("/Password")]
    public class AuthenticattionController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        public AuthenticattionController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<AuthenticatedResponse> LogIn([FromBody] CheckPasswordeDto pas)
        {
            var ok = _authenticationService.CheckPassword(pas.mail, pas.paswd);
            var usr = new UserDto();
            if (ok)
            {
                usr = _userService.GetUserByEmail(pas.mail);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey###123@@@"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString()),
                new Claim(ClaimTypes.Name, usr.FirstName + " " + usr.LastName),
                new Claim(ClaimTypes.Role, usr.Role)
                };
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7221",
                    audience: "https://localhost:4200",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { 
                    Token = tokenString,
                    Exp = 24.ToString(),
                    User = usr
                 });
            }
            return Unauthorized();
        }
    }
}

