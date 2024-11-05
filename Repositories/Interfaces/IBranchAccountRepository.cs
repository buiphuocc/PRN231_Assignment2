using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBranchAccountRepository
    {
        Task<BranchAccount> AddBranchAccountAsync(BranchAccount branchAccount);
        Task<BranchAccount?> GetBranchAccountByIdAsync(int accountId);
        Task<IEnumerable<BranchAccount>> GetAllBranchAccountsAsync();
        Task<BranchAccount?> UpdateBranchAccountAsync(BranchAccount updatedAccount);
        Task<bool> DeleteBranchAccountAsync(int accountId);
        Task<BranchAccount?> AuthenticateAsync(string email, string password);
    }
}
