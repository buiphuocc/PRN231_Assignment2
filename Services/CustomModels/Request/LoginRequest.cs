using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomModels.Request
{
    public class LoginRequest
    {
        public string? EmailAddress { get; set; }

        public string AccountPassword { get; set; } = null!;
    }
}
