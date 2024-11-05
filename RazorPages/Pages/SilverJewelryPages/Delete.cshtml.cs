using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Data;
using Repositories.Entities;

namespace RazorPages.Pages.SilverJewelryPages
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string UserRole { get; set; }

        public string SilverJewelryId { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SilverJewelryId = id;

            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");

            UserRole = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(token))
            {
                // Redirect to login if token is missing
                return RedirectToPage("/Pages/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string silverJewelryId)
        {
            if (silverJewelryId == null)
            {
                return NotFound();
            }

            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");
            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //Call delete Api
            var url = "http://localhost:5165/api/silver-jewelry?jewelryId=" + silverJewelryId;
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/SilverJewelryPages/Index"); // Change this to your desired redirect page

            }

            // Handle login failure
            ModelState.AddModelError(string.Empty, "Invalid delete attempt.");
            return Page();
        }
    }
}
