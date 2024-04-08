using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristopherChurch.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
//TODO Create Authentication for users to login
namespace ChristopherChurch.Data.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IConfiguration _configuration;
        private readonly string _googleCalendarId;
        private readonly AuthenticationStateProvider _authStateProvider;

        public GoogleCalendarService(AuthenticationStateProvider authStateProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _googleCalendarId = _configuration["GoogleApi:CalendarId"];
            _authStateProvider = authStateProvider;
        }

        public async Task<List<EventModel>> GetPublicEventsAsync()
        {
            try
            {
                var service = await GetCalendarServiceAsync();

                var request = service.Events.List(_googleCalendarId);
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var events = await request.ExecuteAsync();

                var eventModels = new List<EventModel>();

                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        if (IsEventPublic(eventItem))
                        {
                            var eventModel = new EventModel
                            {
                                EventId = eventItem.Id,
                                Summary = eventItem.Summary,
                                Description = eventItem.Description,
                                StartDateTime = eventItem.Start.DateTime ?? DateTime.MinValue,
                                EndDateTime = eventItem.End.DateTime ?? DateTime.MinValue,
                                Location = eventItem.Location,
                                AddToCalendarLink = GetEventAddToGoogleCalendarLink(eventItem)
                            };

                            eventModels.Add(eventModel);
                        }
                    }
                }

                return eventModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                throw;
            }
        }

        private bool IsEventPublic(Event eventItem)
        {
            // Check if the event visibility is set to "public"
            if (eventItem.Visibility != null && eventItem.Visibility.ToLower() == "public")
            {
                return true;
            }

            return false; 
        }

        private async Task<CalendarService> GetCalendarServiceAsync()
        {
            var credential = await GetGoogleCredentials();
            return new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Christopher Church",
            });
        }

        private async Task<UserCredential> GetGoogleCredentials()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity!.IsAuthenticated)
            {
                var accessTokenClaim = user.FindFirst("access_token");

                if (accessTokenClaim != null)
                {
                    return GoogleCredential.FromAccessToken(accessTokenClaim.Value).CreateScoped(CalendarService.Scope.Calendar).UnderlyingCredential as UserCredential;
                }
                else
                {
                    throw new UnauthorizedAccessException("Access token not found in user claims");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
        }
        
        public string GetEventAddToGoogleCalendarLink(Event websiteEvent)
        {
            // Ensure that Start and End properties are not null
            if (websiteEvent.Start == null || websiteEvent.End == null)
            {
                throw new InvalidOperationException("Start or End time of the event is null.");
            }

            // Construct the Google Calendar event URL with the event details
            var baseUrl = "https://www.google.com/calendar/render?action=TEMPLATE";
            var summary = Uri.EscapeDataString(websiteEvent.Summary);
            var description = Uri.EscapeDataString(websiteEvent.Description);
            var location = Uri.EscapeDataString(websiteEvent.Location);
            var startDateTime = websiteEvent.Start.DateTimeDateTimeOffset.ToString();
            var endDateTime = websiteEvent.End.DateTimeDateTimeOffset.ToString();

            // Construct the complete URL
            var url = $"{baseUrl}&text={summary}&details={description}&location={location}&dates={startDateTime}/{endDateTime}";

            return url;
        }
    }
}
