namespace Huawei.DeveloperApi
{
    using System;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Olive;

    class HuaweiObtainTokenRequest : IHuaweiRequest
    {
        [JsonPropertyName("grant_type")]
        public string GrantType => "client_credentials";

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        public Task Validate()
        {
            if (ClientId.IsEmpty()) throw new ArgumentNullException(nameof(ClientId));

            if (ClientSecret.IsEmpty()) throw new ArgumentNullException(nameof(ClientSecret));

            return Task.CompletedTask;
        }
    }
}
