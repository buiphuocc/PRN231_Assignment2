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
    public interface ISilverJewelryService
    {
        Task<IEnumerable<SilverJewelryResponse>> GetAll();
        Task<SilverJewelryResponse> AddSilverJewelryAsync(AddSilverJewelryRequest request);
        Task<IEnumerable<SilverJewelryResponse>> SearchSilverJewelriesAsync(string? name, decimal? weight);
        Task<SilverJewelryResponse?> UpdateSilverJewelryAsync(AddSilverJewelryRequest request);
        Task<DeleteSilverJewelryResponse> DeleteSilverJewelryAsync(string silverJewelryId);
    }
}
