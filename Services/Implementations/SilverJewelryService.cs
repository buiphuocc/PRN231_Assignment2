using Repositories.Entities;
using Repositories.Interfaces;
using Services.CustomModels.Request;
using Services.CustomModels.Response;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SilverJewelryService : ISilverJewelryService
    {
        private readonly ISilverJewelryRepository _silverJewelryRepository;

        public SilverJewelryService(ISilverJewelryRepository silverJewelryRepository)
        {
            _silverJewelryRepository = silverJewelryRepository;
        }

        public async Task<SilverJewelryResponse> AddSilverJewelryAsync(AddSilverJewelryRequest request)
        {
            try
            {
                var silverJewelry = new SilverJewelry
                {
                    SilverJewelryId = request.SilverJewelryId,
                    SilverJewelryName = request.SilverJewelryName,
                    SilverJewelryDescription = request.SilverJewelryDescription,
                    MetalWeight = request.MetalWeight,
                    Price = request.Price,
                    ProductionYear = request.ProductionYear,
                    CreatedDate = request.CreatedDate,
                    CategoryId = request.CategoryId,
                };
                var result = await _silverJewelryRepository.AddSilverJewelryAsync(silverJewelry);
                var response = new SilverJewelryResponse
                {
                    SilverJewelryId = result.SilverJewelryId,
                    CategoryName = result.SilverJewelryName,
                    SilverJewelryDescription = result.SilverJewelryDescription,
                    MetalWeight = result.MetalWeight,
                    Price = result.Price,
                    ProductionYear = result.ProductionYear,
                    CreatedDate = result.CreatedDate,
                    SilverJewelryName = result.Category?.CategoryName
                };

                return response;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SilverJewelryResponse>> GetAll()
        {
            try
            {
                var silverJewelries = await _silverJewelryRepository.GetAllSilverJewelriesAsync();
                var response = silverJewelries.Select(sj => new SilverJewelryResponse
                {
                    SilverJewelryId = sj.SilverJewelryId,
                    SilverJewelryName = sj.SilverJewelryName,
                    SilverJewelryDescription = sj.SilverJewelryDescription,
                    MetalWeight = sj.MetalWeight,
                    Price = sj.Price,
                    ProductionYear = sj.ProductionYear,
                    CreatedDate = sj.CreatedDate,
                    CategoryName = sj.Category?.CategoryName
                });

                return response;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SilverJewelryResponse>> SearchSilverJewelriesAsync(string? name, decimal? weight)
        {
            try
            {
                var result = await _silverJewelryRepository.SearchSilverJewelriesAsync(name, weight);
                var response = result.Select(r => new SilverJewelryResponse
                {
                    SilverJewelryId = r.SilverJewelryId,
                    SilverJewelryName = r.SilverJewelryName,
                    SilverJewelryDescription = r.SilverJewelryDescription,
                    MetalWeight = r.MetalWeight,
                    Price = r.Price,
                    ProductionYear = r.ProductionYear,
                    CreatedDate = r.CreatedDate,
                    CategoryName = r.Category?.CategoryName
                });
                return response;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SilverJewelryResponse?> UpdateSilverJewelryAsync(AddSilverJewelryRequest request)
        {
            try
            {
                var silverJewelry = new SilverJewelry
                {
                    SilverJewelryId = request.SilverJewelryId,
                    SilverJewelryName = request.SilverJewelryName,
                    SilverJewelryDescription = request.SilverJewelryDescription,
                    MetalWeight = request.MetalWeight,
                    Price = request.Price,
                    ProductionYear = request.ProductionYear,
                    CreatedDate = request.CreatedDate,
                    CategoryId = request.CategoryId,
                };
                var result = await _silverJewelryRepository.UpdateSilverJewelryAsync(silverJewelry);
                var response = new SilverJewelryResponse
                {
                    SilverJewelryId = result.SilverJewelryId,
                    SilverJewelryName = result.SilverJewelryName,
                    SilverJewelryDescription = result.SilverJewelryDescription,
                    MetalWeight = result.MetalWeight,
                    Price = result.Price,
                    ProductionYear = result.ProductionYear,
                    CreatedDate = result.CreatedDate,
                    CategoryName = result.Category?.CategoryName
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DeleteSilverJewelryResponse> DeleteSilverJewelryAsync(string silverJewelryId)
        {
            try
            {
                var result = await _silverJewelryRepository.DeleteSilverJewelryAsync(silverJewelryId);
                var response = new DeleteSilverJewelryResponse
                {
                    DeleteStatus = result,
                    Message = result ? "Silver jewelry item deleted successfully." : "Failed to delete the silver jewelry item. It may not exist or there was an error."
                };
                return response;
                
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
