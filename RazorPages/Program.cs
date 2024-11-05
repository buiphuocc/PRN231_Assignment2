var builder = WebApplication.CreateBuilder(args);

// Register HttpClient service
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorPages();

//Session config
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the cookie accessible only via HTTP
    options.Cookie.IsEssential = true; // Make the session cookie essential for the app
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

// Set the initial page to the login page
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Login");
    await Task.CompletedTask;
});

app.Run();
