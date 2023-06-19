namespace Huawei.DeveloperApi
{
    using System;
    using System.ComponentModel;
    using System.Text.Json.Serialization;

    public class HuaweiValidateSubscriptionResult : HuaweiResultBase
    {
        [JsonPropertyName("inappPurchaseData")]
        public string? InappPurchaseDataStr { get; set; }

        public HuaweiInappPurchaseData InappPurchaseData
            => InappPurchaseDataStr.FromJson<HuaweiInappPurchaseData>();

        [JsonPropertyName("dataSignature")]
        public string? DataSignature { get; set; }

        [JsonPropertyName("signatureAlgorithm")]
        public string? SignatureAlgorithm { get; set; }
    }

    public class HuaweiInappPurchaseData
    {
        [JsonPropertyName("AutoRenewing")]
        public bool AutoRenewing { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("purchaseToken")]
        public string PurchaseToken { get; set; }

        [JsonPropertyName("purchaseTime")]
        [JsonConverter(typeof(NullableDateTimeOffsetConverter))]
        public DateTimeOffset? PurchaseTime { get; set; }

        [JsonPropertyName("expirationDate")]
        [JsonConverter(typeof(NullableDateTimeOffsetConverter))]
        public DateTimeOffset? ExpirationDate { get; set; }
    }
}
