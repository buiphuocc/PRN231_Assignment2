using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class LoginModel : PageModel
{
    private readonly HttpClient _httpClient;

    public LoginModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public async Task<IActionResult> OnPostAsync(string email, string password)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Create the login request payload
        var loginData = new
        {
            EmailAddress = email, // Assuming username is an email address
            AccountPassword = password
        };

        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        // Use the correct endpoint from your Swagger
        var response = await _httpClient.PostAsync("http://localhost:5165/api/authentication/login", content);

        if (response.IsSuccessStatusCode)
        {
            // Handle successful login
            var responseBody = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

            // Optionally, store the token in a secure way (e.g., session, cookie, local storage)
            HttpContext.Session.SetString("Token", loginResponse.Token);
            HttpContext.Session.SetString("FullName", loginResponse.FullName);
            HttpContext.Session.SetString("Role", loginResponse.Role.ToString());

            if (loginResponse.Role.ToString().Equals("1") || loginResponse.Role.ToString().Equals("2"))
            {
                return RedirectToPage("/SilverJewelryPages/Index"); // Change this to your desired redirect page
            }
            
        }

        // Handle login failure
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    public class LoginResponse
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}
