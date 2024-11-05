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
    public class CategoryDAO
    {
        // Create
        public static async Task<Category> AddCategoryAsync(Category category)
        {
            using var context = new SilverJewelry2023DbContext();
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        // Read by ID
        public static async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            using var context = new SilverJewelry2023DbContext();
            return await context.Categories
                .Include(c => c.SilverJewelries) // Include related entities if needed
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        // Read all
        public static async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            using var context = new SilverJewelry2023DbContext();
            return await context.Categories
                .Include(c => c.SilverJewelries) // Include related entities if needed
                .ToListAsync();
        }

        // Update
        public static async Task<Category?> UpdateCategoryAsync(Category updatedCategory)
        {
            using var context = new SilverJewelry2023DbContext();
            var existingCategory = await context.Categories.FindAsync(updatedCategory.CategoryId);
            if (existingCategory == null) return null;

            existingCategory.CategoryName = updatedCategory.CategoryName;
            existingCategory.CategoryDescription = updatedCategory.CategoryDescription;
            existingCategory.FromCountry = updatedCategory.FromCountry;

            await context.SaveChangesAsync();
            return existingCategory;
        }

        // Delete
        public static async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            using var context = new SilverJewelry2023DbContext();
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
