using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Dto
{
    public interface IPhotoDto
    {
        long Id { get; }

        string Url { get; }

        string Description { get; }

        DateTimeOffset CreatedDate { get; }
    }
}
