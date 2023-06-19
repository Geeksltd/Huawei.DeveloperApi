namespace Huawei.DeveloperApi
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;

    public class HuaweiDeveloperService
    {
        readonly HuaweiOptions Options;
        readonly IHuaweiTokenStorage TokenStorage;
        readonly WebApiInvoker WebApiInvoker;

        public HuaweiDeveloperService(
            IOptionsSnapshot<HuaweiOptions> options,
            IHuaweiTokenStorage tokenStorage
        )
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            TokenStorage = tokenStorage ?? throw new ArgumentNullException(nameof(tokenStorage));
            WebApiInvoker = new WebApiInvoker();
        }

        public async Task<HuaweiValidateSubscriptionResult> ValidateSubscription(HuaweiValidateSubscriptionRequest request)
        {
            await request.Validate();

            await EnsureAccessTokenValidity();

            var accessToken = await TokenStorage.GetAccessToken();
            var path = "https://subscr-dre.iap.cloud.huawei.eu/sub/applications/v2/purchases/get";

            var result = await WebApiInvoker
                .PostJson<HuaweiValidateSubscriptionResult>(path, request, accessToken);

            result.EnsureSucceeded();

            return result;
        }

        async Task EnsureAccessTokenValidity()
        {
            if (await TokenStorage.AccessTokenExpired())
                await ObtainAccessToken();
        }

        async Task ObtainAccessToken()
        {
            var path = "https://oauth-login.cloud.huawei.com/oauth2/v3/token";

            var request = new HuaweiObtainTokenRequest
            {
                ClientId = Options.ClientId,
                ClientSecret = Options.ClientSecret
            };

            await request.Validate();

            var result = await WebApiInvoker
                .PostForm<HuaweiObtainTokenResult>(path, request);

            result.EnsureSucceeded();

            await TokenStorage.Renew(result.AccessToken, result.ExpiresIn);
        }
    }
}
