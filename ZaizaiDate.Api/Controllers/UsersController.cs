using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaizaiDate.Api.ViewModels;
using ZaizaiDate.Business.Service;

namespace ZaizaiDate.Api.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserManagementService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserManagementService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync().ConfigureAwait(false);

            var usersToReturn = _mapper.Map<IEnumerable<UserListViewModel>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetUserAsync(id).ConfigureAwait(false);

            var toReturn = _mapper.Map<UserDetailedViewModel>(user);

            return Ok(toReturn);
        }
    }
}