using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SilverJewelryDAO
    {
        private static bool IsValidSilverJewelryName(string name)
        {
            // Checks if each word starts with an uppercase letter and contains only alphanumeric characters or spaces
            return Regex.IsMatch(name, @"^([A-Z][a-zA-Z0-9-]*)(\s[a-zA-Z0-9-]*)*$");
        }

        // Create
        public static async Task<SilverJewelry?> AddSilverJewelryAsync(SilverJewelry silverJewelry)
        {
            using var context = new SilverJewelry2023DbContext();
            // Validation for SilverJewelryName
            if (!IsValidSilverJewelryName(silverJewelry.SilverJewelryName))
            {
                throw new ArgumentException("SilverJewelryName must contain words starting with a capital letter, containing letters and numbers.");
            }

            // Validation for ProductionYear
            if (silverJewelry.ProductionYear < 1900)
            {
                throw new ArgumentException("ProductionYear must be greater than or equal to 1900.");
            }

            // Validation for Price
            if (silverJewelry.Price < 0)
            {
                throw new ArgumentException("Price must be greater than or equal to 0.");
            }

            // Set CreatedDate to current date
            silverJewelry.CreatedDate = DateTime.UtcNow;

            // Add the validated SilverJewelry entity to the context
            context.SilverJewelries.Add(silverJewelry);
            await context.SaveChangesAsync();
            return silverJewelry;
        }

        // Read by ID
        public static async Task<SilverJewelry?> GetSilverJewelryByIdAsync(string silverJewelryId)
        {
            using var context = new SilverJewelry2023DbContext();
            return await context.SilverJewelries
                .Include(sj => sj.Category) // Include related Category if needed
                .FirstOrDefaultAsync(sj => sj.SilverJewelryId == silverJewelryId);
        }

        // Read all
        public static async Task<IEnumerable<SilverJewelry>> GetAllSilverJewelriesAsync()
        {
            using var context = new SilverJewelry2023DbContext();
            return await context.SilverJewelries
                .Include(sj => sj.Category) // Include related Category if needed
                .ToListAsync();
        }

        //Relative search
        public static async Task<IEnumerable<SilverJewelry>> SearchSilverJewelriesAsync(string? name = null, decimal? weight = null)
        {
            using var context = new SilverJewelry2023DbContext();
            var query = context.SilverJewelries.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(sj => sj.SilverJewelryName.Contains(name));
            }

            if (weight.HasValue)
            {
                query = query.Where(sj => sj.MetalWeight.HasValue && sj.MetalWeight.Value == weight.Value);
            }

            return await query.Include(sj => sj.Category).ToListAsync();
        }

        // Update
        public static async Task<SilverJewelry?> UpdateSilverJewelryAsync(SilverJewelry updatedSilverJewelry)
        {
            using var context = new SilverJewelry2023DbContext();
            var existingJewelry = await context.SilverJewelries.FindAsync(updatedSilverJewelry.SilverJewelryId);
            if (existingJewelry == null) return null;

            // Re-apply validations on the update
            if (string.IsNullOrWhiteSpace(updatedSilverJewelry.SilverJewelryName) || !IsValidSilverJewelryName(updatedSilverJewelry.SilverJewelryName))
            {
                throw new ArgumentException("SilverJewelryName must contain words starting with a capital letter, containing only letters and numbers.");
            }

            if (updatedSilverJewelry.ProductionYear < 1900)
            {
                throw new ArgumentException("ProductionYear must be greater than or equal to 1900.");
            }

            if (updatedSilverJewelry.Price < 0)
            {
                throw new ArgumentException("Price must be greater than or equal to 0.");
            }

            existingJewelry.SilverJewelryName = updatedSilverJewelry.SilverJewelryName;
            existingJewelry.SilverJewelryDescription = updatedSilverJewelry.SilverJewelryDescription;
            existingJewelry.MetalWeight = updatedSilverJewelry.MetalWeight;
            existingJewelry.Price = updatedSilverJewelry.Price;
            existingJewelry.ProductionYear = updatedSilverJewelry.ProductionYear;
            existingJewelry.CreatedDate = DateTime.UtcNow;
            existingJewelry.CategoryId = updatedSilverJewelry.CategoryId;

            await context.SaveChangesAsync();
            return existingJewelry;
        }

        // Delete
        public static async Task<bool> DeleteSilverJewelryAsync(string silverJewelryId)
        {
            using var context = new SilverJewelry2023DbContext();
            var silverJewelry = await context.SilverJewelries.FindAsync(silverJewelryId);
            if (silverJewelry == null) return false;

            context.SilverJewelries.Remove(silverJewelry);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
