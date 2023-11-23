using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ChristopherChurch.UI.Data;
using ChristopherChurch.Data.DbAccess;
using ChristopherChurch.Data.DataAccess;
using Stripe;
using Microsoft.Extensions.DependencyInjection;
using ChristopherChurch.UI.Models;
using ChristopherChurch.UI.Pages;
using System.Configuration;
using ChristopherChurch.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IPersonData, PersonData>();
builder.Services.AddTransient<IEmailService, EmailService>();


//builder.Services.AddStripe(Configuration.GetSection("Stripe"));

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

app.UseRouting();

// Stripe exception handling middleware
//app.UseStripeExceptionHandler();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

