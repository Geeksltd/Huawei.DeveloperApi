namespace Huawei.DeveloperApi
{
    using Olive;
    using System;
    using System.Threading.Tasks;

    public abstract class HuaweiTokenStorageBase : IHuaweiTokenStorage
    {
        public async Task<string> GetAccessToken() => (await GetToken()).AccessToken;

        public async Task<bool> AccessTokenExpired() => (await GetToken()).ExpiresAt < LocalTime.Now;

        public abstract Task Renew(string accessToken, TimeSpan expiresIn);

        protected abstract Task<HuaweiToken> GetToken();
    }
}
