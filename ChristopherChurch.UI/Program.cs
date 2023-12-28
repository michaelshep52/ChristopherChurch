﻿using ChristopherChurch.UI.Data;
using ChristopherChurch.Data.DbAccess;
using ChristopherChurch.Data.DataAccess;
using ChristopherChurch.Data.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IPersonData, PersonData>();
builder.Services.AddTransient<IEventsData, EventsData>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IMinistryFormService,MinistryFormService>();

var app = builder.Build();

QuestPDF.Settings.License = LicenseType.Community;

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


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

