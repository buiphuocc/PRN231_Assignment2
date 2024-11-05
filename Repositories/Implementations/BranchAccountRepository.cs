using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Data;
using Repositories.Interfaces;
using DataAccessLayer;

namespace Repositories.Implementations
{
    public class BranchAccountRepository : IBranchAccountRepository
    {
        public async Task<BranchAccount> AddBranchAccountAsync(BranchAccount branchAccount)
        {
            return await BranchAccountDAO.AddBranchAccountAsync(branchAccount);
        }

        public async Task<BranchAccount?> AuthenticateAsync(string email, string password)
        {
            return await BranchAccountDAO.AuthenticateAsync(email, password);
        }

        public async Task<bool> DeleteBranchAccountAsync(int accountId)
        {
            return await BranchAccountDAO.DeleteBranchAccountAsync(accountId);
        }

        public async Task<IEnumerable<BranchAccount>> GetAllBranchAccountsAsync()
        {
            return await BranchAccountDAO.GetAllBranchAccountsAsync();
        }

        public async Task<BranchAccount?> GetBranchAccountByIdAsync(int accountId)
        {
            return await BranchAccountDAO.GetBranchAccountByIdAsync(accountId);
        }

        public async Task<BranchAccount?> UpdateBranchAccountAsync(BranchAccount updatedAccount)
        {
            return await BranchAccountDAO.UpdateBranchAccountAsync(updatedAccount);
        }
    }
}

