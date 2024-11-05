using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class SilverJewelryRepository : ISilverJewelryRepository
    {
        public async Task<SilverJewelry> AddSilverJewelryAsync(SilverJewelry silverJewelry)
        {
            return await SilverJewelryDAO.AddSilverJewelryAsync(silverJewelry);
        }

        public async Task<bool> DeleteSilverJewelryAsync(string silverJewelryId)
        {
            return await SilverJewelryDAO.DeleteSilverJewelryAsync(silverJewelryId);
        }

        public async Task<IEnumerable<SilverJewelry>> GetAllSilverJewelriesAsync()
        {
            return await SilverJewelryDAO.GetAllSilverJewelriesAsync();
        }

        public async Task<SilverJewelry?> GetSilverJewelryByIdAsync(string silverJewelryId)
        {
            return await SilverJewelryDAO.GetSilverJewelryByIdAsync(silverJewelryId);
        }

        public async Task<IEnumerable<SilverJewelry>> SearchSilverJewelriesAsync(string? name = null, decimal? weight = null)
        {
            return await SilverJewelryDAO.SearchSilverJewelriesAsync(name, weight);
        }

        public async Task<SilverJewelry?> UpdateSilverJewelryAsync(SilverJewelry updatedSilverJewelry)
        {
            return await SilverJewelryDAO.UpdateSilverJewelryAsync(updatedSilverJewelry);
        }
    }
}
