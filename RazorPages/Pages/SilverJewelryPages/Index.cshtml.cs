using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Data;
using Repositories.Entities;
using Services.CustomModels.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RazorPages.Pages.SilverJewelryPages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<SilverJewelry> SilverJewelry { get; set; } = default!;
        public string UserRole { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchWeight { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");
            
            UserRole = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(token))
            {
                // Redirect to login if token is missing
                return RedirectToPage("/Login");
            }

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Build API endpoint with optional search query
            var baseUrl = "http://localhost:5165/odata/SilverJewelry";
            // Add the filters based on user input
            var filters = new List<string>();

            if (!string.IsNullOrEmpty(SearchName))
            {
                // Add the filter for SilverJewelryName containing the search string
                filters.Add($"contains(tolower(SilverJewelryName), tolower('{SearchName}'))");
            }

            if (!string.IsNullOrEmpty(SearchWeight))
            {
                filters.Add($"MetalWeight ge {SearchWeight}");
            }

            // Combine filters with 'and'
            if (filters.Any())
            {
                baseUrl += "?$filter=" + string.Join(" and ", filters);
                baseUrl += "&expand=Category";
            }
            else
            {
                baseUrl += "?expand=Category";
            }

            

            var response = await _httpClient.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content if successful
                var silverJewelryResponse = JsonConvert.DeserializeObject<SilverJewelryResponse>(await response.Content.ReadAsStringAsync());
                SilverJewelry = silverJewelryResponse.Value;
                return Page();
            }
            else
            {
                // Handle failure with appropriate status code
                return StatusCode((int)response.StatusCode);
            }
        }

        public class SilverJewelryResponse
        {
            public string OdataContext { get; set; }
            public IList<SilverJewelry> Value { get; set; }
        }
    }

}
