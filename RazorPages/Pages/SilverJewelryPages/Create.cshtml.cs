using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Repositories.Data;
using Repositories.Entities;
using NuGet.Common;
using System.Security.Policy;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text;

namespace RazorPages.Pages.SilverJewelryPages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string UserRole { get; set; }

        public IActionResult OnGet()
        {
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

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");
            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            
            //Serialize Object
            var content = new StringContent(JsonConvert.SerializeObject(SilverJewelry), Encoding.UTF8, "application/json");

            //Call add Api
            var url = "http://localhost:5165/api/silver-jewelry";
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/SilverJewelryPages/Index"); // Change this to your desired redirect page
                
            }

            // Handle login failure
            ModelState.AddModelError(string.Empty, "Invalid add attempt.");
            return Page();
        }
    }
}
