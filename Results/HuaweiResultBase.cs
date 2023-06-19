namespace Huawei.DeveloperApi
{
    using Olive;
    using System;
    using System.Text.Json.Serialization;

    public abstract class HuaweiResultBase
    {
        [JsonPropertyName("responseCode")]
        public string ResponseCode { private get; set; }

        [JsonPropertyName("responseMessage")]
        public string? ResponseMessage { private get; set; }

        bool Failed => ResponseCode.IsNoneOf(null, "0");

        internal void EnsureSucceeded()
        {
            if (Failed)
                throw new Exception($"{ResponseCode}");
        }
    }
}
