using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISilverJewelryRepository
    {
        Task<SilverJewelry> AddSilverJewelryAsync(SilverJewelry silverJewelry);
        Task<SilverJewelry?> GetSilverJewelryByIdAsync(string silverJewelryId);
        Task<IEnumerable<SilverJewelry>> GetAllSilverJewelriesAsync();
        Task<IEnumerable<SilverJewelry>> SearchSilverJewelriesAsync(string? name = null, decimal? weight = null);
        Task<SilverJewelry?> UpdateSilverJewelryAsync(SilverJewelry updatedSilverJewelry);
        Task<bool> DeleteSilverJewelryAsync(string silverJewelryId);
    }
}
