using ChristopherChurch.Data.DataAccess;
using ChristopherChurch.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChristopherChurch.Data.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private readonly string ApplicationName = "Christopher Church";

        private readonly IConfiguration _configuration;
        private readonly IEventsData _db;

        public GoogleCalendarService(IConfiguration configuration, IEventsData db)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task AddEventAsync(string eventName, string userToken)
        {
            try
            {
                // Use _db service to get the event details from the database
                var databaseEvent = await _db.GetEventByName(eventName);

                if (databaseEvent != null)
                {
                    // Create and fill the event details from the database
                    var calendarEvent = new Event
                    {
                        Summary = databaseEvent.EventName,
                        Description = databaseEvent.Description,
                        Start = new EventDateTime { DateTime = databaseEvent.EventDate, TimeZone = "Eastern Standard Time" },
                        End = new EventDateTime { DateTime = databaseEvent.EventDate.AddHours(1), TimeZone = "Eastern Standard Time" },
                    };

                    // Call the backend service to add the event to Google Calendar
                    await AddEventToGoogleCalendar(calendarEvent, userToken);

                    // Handle any additional logic after adding the event
                }
                else
                {
                    Console.WriteLine("Error: Event not found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding event to Google Calendar: {ex.Message}");
            }
        }

        private async Task AddEventToGoogleCalendar(Event calendarEvent, string userToken)
        {
            // Retrieve sensitive data from appsettings.json
            var credentialsPath = _configuration["GoogleApi:Credentials"];

            // Use ClientSecrets directly
            var clientSecrets = await GoogleClientSecrets.FromFileAsync(credentialsPath);

            var credential = new UserCredential(new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = clientSecrets.Secrets, // Use Secrets directly
                    Scopes = Scopes,
                }),
                "user",
                new TokenResponse { AccessToken = userToken });

            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Insert the event
            await service.Events.Insert(calendarEvent, "primary").ExecuteAsync();
        }
    }
}
