using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZaizaiDate.Api.ViewModels;
using ZaizaiDate.Business.Service;
using ZaizaiDate.Common.Settings;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Api.Controllers
{ 
    public class AuthController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISecretSettings _secretSettings;

        public AuthController(IAuthenticationService authenticationService, ISecretSettings settings)
        {
            _authenticationService = authenticationService;
            _secretSettings = settings;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel user)
        {
            if (user is null)
            {
                return BadRequest("User information is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)));
            }

            if (await _authenticationService.UserExists(user.UserName).ConfigureAwait(false))
                return BadRequest("User already exists");

            await _authenticationService.RegisterAsync(user).ConfigureAwait(false);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)));
            }

            var userLogin =  await _authenticationService.LoginAsync(user).ConfigureAwait(false);
            if (userLogin == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim("uid", userLogin.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim("uname", userLogin.UserName)
            };
             
            string jwtSigningKey = _secretSettings.JwtSigningKey;
            var symmetricKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = credentials,
                Audience = "http://localhost",
                Issuer = "http://localhost"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { authToken = tokenHandler.WriteToken(token) });
        }


   
        [AllowAnonymous]
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            return Ok( new { Time = DateTimeOffset.Now.ToString(CultureInfo.CurrentCulture) });
        }
    }
}