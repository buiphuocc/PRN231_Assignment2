using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<Category> AddCategoryAsync(Category category)
        {
            return await CategoryDAO.AddCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            return await CategoryDAO.DeleteCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await CategoryDAO.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await CategoryDAO.GetCategoryByIdAsync(categoryId);
        }

        public async Task<Category?> UpdateCategoryAsync(Category updatedCategory)
        {
            return await CategoryDAO.UpdateCategoryAsync(updatedCategory);
        }
    }
}
