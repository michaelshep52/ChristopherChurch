using ChristopherChurch.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;

namespace ChristopherChurch.Data.Services
{
    public class GoogleCalendarService
    {
        private readonly CalendarService _calendarService;
        private readonly string _tokenPath = "token.json"; // Change this path as needed

        public GoogleCalendarService()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string clientId = configuration["GoogleCalendar:ClientId"] ?? throw new InvalidOperationException("ClientId is not configured");
            string clientSecret = configuration["GoogleCalendar:ClientSecret"] ?? throw new InvalidOperationException("ClientSecret is not configured");
            string redirectUri = configuration["GoogleCalendar:RedirectUri"] ?? throw new InvalidOperationException("RedirectUri is not configured");
            _tokenPath = configuration["GoogleCalendar:TokenPath"] ?? throw new InvalidOperationException("TokenPath is not configured");


            _calendarService = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredentials(clientId, clientSecret, redirectUri),
                ApplicationName = "YourAppName",
            });
        }

        private UserCredential GetCredentials(string clientId, string clientSecret, string redirectUri)
        {
            UserCredential credential;

            using (var stream = new FileStream(_tokenPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                    },
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(_tokenPath, true)).Result;
            }

            return credential;
        }

        public void AddEventToGoogleCalendar(EventModel eventModel)
        {
            try
            {
                // Create an event request
                Event newEvent = new Event
                {
                    Summary = eventModel.EventName,
                    Description = eventModel.Description,
                    Start = new EventDateTime { DateTimeDateTimeOffset = eventModel.EventDate, TimeZone = "UTC" },
                    End = new EventDateTime { DateTimeDateTimeOffset = eventModel.EventDate.AddHours(1), TimeZone = "UTC" },
                };

                // Insert the event
                EventsResource.InsertRequest request = _calendarService.Events.Insert(newEvent, "primary");
                Event createdEvent = request.Execute();

                Console.WriteLine($"Event created: {createdEvent.HtmlLink}");

                // You can return the createdEvent or its link as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating event: {ex.Message}");
                // Handle the exception, log it, or provide user feedback
            }
        }
    }
}
