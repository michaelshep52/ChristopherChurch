using ChristopherChurch.UI.Data;
using ChristopherChurch.Data.DbAccess;
using ChristopherChurch.Data.DataAccess;
using ChristopherChurch.Data.Services;
using QuestPDF.Infrastructure;
using ChristopherChurch.Data.Models;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IEventsData, EventsData>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IMinistryFormService,MinistryFormService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    try
    {
        var domain = builder.Configuration["Auth0:Domain"];
        var clientId = builder.Configuration["Auth0:ClientId"];

        if (domain is not null && clientId is not null)
        {
            options.Domain = domain;
            options.ClientId = clientId;
        }
        else
        {
            throw new InvalidOperationException("Auth0 domain or clientId is not configured.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during Auth0 configuration: {ex.Message}");
    }
});

var emailSettings = new EmailSettings();
builder.Configuration.Bind("EmailSettings", emailSettings);
builder.Services.Configure<EmailSettings>(options => builder.Configuration.GetSection("EmailSettings").Bind(emailSettings));

var googleApiSettings = new GoogleApiSettings();
builder.Configuration.Bind("GoogleApi", googleApiSettings);  
builder.Services.Configure<GoogleApiSettings>(options => builder.Configuration.GetSection("GoogleApi").Bind(googleApiSettings));

QuestPDF.Settings.License = LicenseType.Community;

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

