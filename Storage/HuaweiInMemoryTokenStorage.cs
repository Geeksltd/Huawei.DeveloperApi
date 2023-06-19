namespace Huawei.DeveloperApi
{
    using System;
    using System.Threading.Tasks;
    using Olive;

    public class HuaweiInMemoryTokenStorage : HuaweiTokenStorageBase
    {
        readonly HuaweiToken Token;

        public HuaweiInMemoryTokenStorage() => Token = new HuaweiToken();

        public override Task Renew(string accessToken, TimeSpan expiresIn)
        {
            if (accessToken.IsEmpty()) throw new ArgumentNullException(nameof(accessToken));

            if (expiresIn == TimeSpan.MinValue) throw new ArgumentException("Already expired!", nameof(expiresIn));

            Token.AccessToken = accessToken;
            Token.ExpiresAt = expiresIn.ToDateTime();

            return Task.CompletedTask;
        }

        protected override Task<HuaweiToken> GetToken() => Task.FromResult(Token);
    }
}
