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

namespace RazorPages.Pages.SilverJewelryPages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<SilverJewelryResponse> SilverJewelry { get; set; } = default!;
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
            var baseUrl = "http://localhost:5165/api/silver-jewelry/search";
            var queryParams = new List<string>();

            // Add each parameter to the query string if it has a value
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                queryParams.Add($"name={SearchName}");
            }

            if (!string.IsNullOrWhiteSpace(SearchWeight))
            {
                queryParams.Add($"weight={SearchWeight}");
            }

            // Construct the final URL with the base URL and query parameters
            var url = queryParams.Count > 0 ? $"{baseUrl}?{string.Join("&", queryParams)}" : baseUrl;

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content if successful
                SilverJewelry = JsonConvert.DeserializeObject<IList<SilverJewelryResponse>>(await response.Content.ReadAsStringAsync());
                return Page();
            }
            else
            {
                // Handle failure with appropriate status code
                return StatusCode((int)response.StatusCode);
            }
        }
    }

}
