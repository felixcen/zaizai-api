using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaizaiDate.Api.ViewModels;
using ZaizaiDate.Business.Service;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Api.Controllers
{ 
    public class LoginController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel user)
        {

            if (await _authenticationService.UserExists(user.UserName).ConfigureAwait(false))
                return BadRequest("User already exists");

            await _authenticationService.RegisterAsync(user).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("hi")]
        public IActionResult Hi()
        { 
            return Ok("Hi");
        }
    }
}