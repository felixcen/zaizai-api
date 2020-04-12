using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZaizaiDate.Common.Dto;

namespace ZaizaiDate.Api.ViewModels
{
    public class LoginViewModel : IUserLoginDto
    {
        [Required] 
        [StringLength(256, MinimumLength = 4)]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required] 
        [StringLength(256, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
