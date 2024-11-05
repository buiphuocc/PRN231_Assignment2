using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Data;
using Repositories.Entities;

namespace RazorPages.Pages.SilverJewelryPages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string UserRole { get; set; }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Retrieve the JWT token and role from session
            var token = HttpContext.Session.GetString("Token");

            UserRole = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(token))
            {
                // Redirect to login if token is missing
                return RedirectToPage("/Pages/Login");
            }

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //Serialize Object
            var content = new StringContent(JsonConvert.SerializeObject(SilverJewelry), Encoding.UTF8, "application/json");

            //Call getAll Api
            var url = "http://localhost:5165/api/silver-jewelry";
            var response = await _httpClient.GetAsync(url);
            var jewelries = new List<SilverJewelry>();
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content if successful
                jewelries = JsonConvert.DeserializeObject<IList<SilverJewelry>>(await response.Content.ReadAsStringAsync()).ToList();

            }

            var silverjewelry =  jewelries.FirstOrDefault(m => m.SilverJewelryId == id);
            if (silverjewelry == null)
            {
                return NotFound();
            }
            SilverJewelry = silverjewelry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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


            //Serialize Object
            var content = new StringContent(JsonConvert.SerializeObject(SilverJewelry), Encoding.UTF8, "application/json");

            //Call add Api
            var url = "http://localhost:5165/api/silver-jewelry";
            var response = await _httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/SilverJewelryPages/Index"); // Change this to your desired redirect page

            }

            return RedirectToPage("./Index");
        }
    }
}
