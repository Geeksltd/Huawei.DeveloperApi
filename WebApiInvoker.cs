namespace Huawei.DeveloperApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Olive;

    class WebApiInvoker
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public TimeSpan Timeout { get; set; } = 30.Seconds();

        public Task<T> PostJson<T>(string path, object request, string accessToken)
            where T : HuaweiResultBase, new()
        {
            return Send<T>(async (client, enc) =>
            {
                var accessTokenStr = Convert.ToBase64String(Encoding.UTF8.GetBytes($"APPAT:{accessToken}"));

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", accessTokenStr);

                var payload = new StringContent(request.ToJson(), Encoding, "application/json");

                return await client.PostAsync(path, payload);
            });
        }

        public Task<T> PostForm<T>(string path, object request)
            where T : HuaweiResultBase, new()
        {
            return Send<T>(async (client, enc) =>
            {
                var payload = new FormUrlEncodedContent(request.ToDictionary());
                return await client.PostAsync(path, payload);
            });
        }

        async Task<T> Send<T>(Func<HttpClient, Encoding, Task<HttpResponseMessage>> requestInitiator) where T : HuaweiResultBase, new()
        {
            try
            {
                var client = CreateClient();

                var message = await requestInitiator(client, Encoding);

                return Encoding.GetString(await message.Content.ReadAsByteArrayAsync()).FromJson<T>();
            }
            catch (Exception ex)
            {
                return CreateDefault<T>(ex);
            }
        }

        T CreateDefault<T>(Exception ex) where T : HuaweiResultBase, new() => new()
        {
            ResponseCode = "unhandled_exception",
            ResponseMessage = ex.Message
        };

        HttpClient CreateClient() => new()
        {
            Timeout = Timeout
        };
    }
}
