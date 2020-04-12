using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaizaiDate.Common.Dto;

namespace ZaizaiDate.Api.ViewModels
{
    public class PhotoViewModel : IPhotoDto
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
