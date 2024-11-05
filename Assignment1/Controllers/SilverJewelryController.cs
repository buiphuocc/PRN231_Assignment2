using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CustomModels.Request;
using Services.Interfaces;

namespace Assignment1.Controllers
{
    [Route("api/silver-jewelry")]
    [ApiController]
    public class SilverJewelryController : ControllerBase
    {
        private readonly ISilverJewelryService _silverJewelryService;

        public SilverJewelryController(ISilverJewelryService silverJewelryService)
        {
            _silverJewelryService = silverJewelryService;
        }

        
        [HttpGet]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> GetAllJewelry()
        {
            try
            {
                var response = await _silverJewelryService.GetAll();
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchJewelry([FromQuery] string? name = null, [FromQuery] decimal? weight = null)
        {
            try
            {
                var response = await _silverJewelryService.SearchSilverJewelriesAsync(name, weight);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddJewelry(AddSilverJewelryRequest request)
        {
            try
            {
                var response = await _silverJewelryService.AddSilverJewelryAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateJewelry(AddSilverJewelryRequest request)
        {
            try
            {
                var response = await _silverJewelryService.UpdateSilverJewelryAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdministratorPolicy")]
        [HttpDelete]
        public async Task<IActionResult> DeleteJewelry(string jewelryId)
        {
            try
            {
                var response = await _silverJewelryService.DeleteSilverJewelryAsync(jewelryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
