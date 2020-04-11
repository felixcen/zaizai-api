using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Settings
{
    public class SecretSettings : ISecretSettings
    {
        public string JwtSigningKey { get; set; }
        public string DatabaseConnectionString { get; set; }
    }
}
