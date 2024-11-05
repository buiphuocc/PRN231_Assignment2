using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(string categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> UpdateCategoryAsync(Category updatedCategory);
        Task<bool> DeleteCategoryAsync(string categoryId);
    }
}
