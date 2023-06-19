namespace Huawei.DeveloperApi
{
    using System;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Olive;

    public class HuaweiValidateSubscriptionRequest : IHuaweiRequest
    {
        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonPropertyName("purchaseToken")]
        public string PurchaseToken { get; set; }

        public Task Validate()
        {
            if (SubscriptionId.IsEmpty()) throw new ArgumentNullException(nameof(SubscriptionId));

            if (PurchaseToken.IsEmpty()) throw new ArgumentNullException(nameof(PurchaseToken));

            return Task.CompletedTask;
        }
    }
}
