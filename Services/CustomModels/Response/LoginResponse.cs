using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomModels.Response
{
    public class LoginResponse
    {
        public string FullName { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public int? Role { get; set; }

        public string? Token { get; set; }
    }
}
