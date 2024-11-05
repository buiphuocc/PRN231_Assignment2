using Repositories.Entities;
using Services.CustomModels.Request;
using Services.CustomModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBranchAccountService
    {
        Task<LoginResponse?> AuthenticateAsync(LoginRequest request);
    }
}
