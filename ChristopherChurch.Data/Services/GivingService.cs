using Stripe;
using Microsoft.Extensions.Configuration;

namespace ChristopherChurch.Data.Services
{

    public class GivingService
    {
        private readonly IConfiguration _configuration;

        public GivingService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["sk_test_51O6z4WJ4ro8u8xiRZAbBqPvEVACUbbGF7QM5TwMNy0oqwsA27JPy6bPqwMeFyFL5izHb0D2OLdNZuNwRHLHvI2uX00A8rMkocN"];
        }

        public async Task<string> CreateGivingIntent(int amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currency,
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent.ClientSecret;
        }

        public async Task<PaymentIntent> ConfirmPaymentIntent(string clientSecret)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.ConfirmAsync(clientSecret);

            return paymentIntent;
        }
    }
}
