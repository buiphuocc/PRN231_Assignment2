using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
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
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                // Redirect to login if token is missing
                return RedirectToPage("/Pages/Login");
            }

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Call get Api
            var url = $"http://localhost:5165/odata/SilverJewelry?$filter=SilverJewelryId eq '{id}'&expand=Category";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content if successful
                var silverJewelryResponse = JsonConvert.DeserializeObject<SilverJewelryResponse>(await response.Content.ReadAsStringAsync());
                SilverJewelry = silverJewelryResponse.Value[0];
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
