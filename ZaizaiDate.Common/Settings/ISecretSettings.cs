using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Settings
{
    public interface ISecretSettings
    {
        string JwtSigningKey { get; }
        string DatabaseConnectionString { get; }
    }
}
