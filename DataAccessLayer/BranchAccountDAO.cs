using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BranchAccountDAO
    {
        // Create
        public static async Task<BranchAccount> AddBranchAccountAsync(BranchAccount branchAccount)
        {
            using var context = new SilverJewelry2023DbContext();
            context.BranchAccounts.Add(branchAccount);
            await context.SaveChangesAsync();
            return branchAccount;
        }

        // Read
        public static async Task<BranchAccount?> GetBranchAccountByIdAsync(int accountId)
        {
            using var context = new SilverJewelry2023DbContext();
            return await context.BranchAccounts.FindAsync(accountId);
        }

        public static async Task<IEnumerable<BranchAccount>> GetAllBranchAccountsAsync()
        {
            using var context = new SilverJewelry2023DbContext();
            var result = await context.BranchAccounts.ToListAsync();
            return result;
        }

        // Update
        public static async Task<BranchAccount?> UpdateBranchAccountAsync(BranchAccount updatedAccount)
        {
            using var context = new SilverJewelry2023DbContext();
            var existingAccount = await context.BranchAccounts.FindAsync(updatedAccount.AccountId);
            if (existingAccount == null) return null;

            existingAccount.AccountPassword = updatedAccount.AccountPassword;
            existingAccount.FullName = updatedAccount.FullName;
            existingAccount.EmailAddress = updatedAccount.EmailAddress;
            existingAccount.Role = updatedAccount.Role;

            await context.SaveChangesAsync();
            return existingAccount;
        }

        // Delete
        public static async Task<bool> DeleteBranchAccountAsync(int accountId)
        {
            using var context = new SilverJewelry2023DbContext();
            var branchAccount = await context.BranchAccounts.FindAsync(accountId);
            if (branchAccount == null) return false;

            context.BranchAccounts.Remove(branchAccount);
            await context.SaveChangesAsync();
            return true;
        }

        //Authenticate
        public static async Task<BranchAccount?> AuthenticateAsync(string email, string password)
        {
            using var context = new SilverJewelry2023DbContext();
            var result = await context.BranchAccounts
                .FirstOrDefaultAsync(account => account.EmailAddress == email && account.AccountPassword == password);
            return result;
        }
    }
}
