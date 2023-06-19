namespace Huawei.DeveloperApi
{
    using System;
    using System.Text.Json.Serialization;

    public class HuaweiObtainTokenResult : HuaweiResultBase
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan ExpiresIn { get; set; }
    }
}
