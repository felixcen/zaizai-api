﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaizaiDate.Common.Dto;

namespace ZaizaiDate.Api.ViewModels
{
    public class LoginViewModel : IUserLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
