using Stripe;

namespace ChristopherChurch.UI.Models
{
    public class GivingService
    {
        private readonly string _secretKey;

        public GivingService(string secretKey)
        {
            _secretKey = secretKey;
            StripeConfiguration.ApiKey = secretKey;
        }

        public string CreateGivingIntent(int amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currency,
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return paymentIntent.ClientSecret;
        }
    }
}