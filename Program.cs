var builder = WebApplication.CreateBuilder(args);

// Register WeatherService with HttpClient
builder.Services.AddHttpClient<WeatherService>();

// Add services to the container.
builder.Services.AddRazorPages();

// Add controllers to the container (necessary for API endpoints).
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Enable HSTS for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map Razor Pages (for your existing pages).
app.MapRazorPages();

// Map Controllers (for API endpoints).
app.MapControllers();

app.Run();
