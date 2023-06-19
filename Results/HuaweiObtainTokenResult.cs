namespace Huawei.DeveloperApi
{
    using System;
    using System.Text;
    using System.Text.Json.Serialization;

    public class HuaweiObtainTokenResult : HuaweiResultBase
    {
        [JsonPropertyName("access_token")]
        public string AccessTokenRaw { get; set; }

        [JsonPropertyName("expires_in")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan ExpiresIn { get; set; }

        public string AccessToken
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(AccessTokenRaw));
    }
}
