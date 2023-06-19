namespace Huawei.DeveloperApi
{
    using System;
    using System.Threading.Tasks;

    public interface IHuaweiTokenStorage
    {
        Task<string> GetAccessToken();
        Task<bool> AccessTokenExpired();

        Task Renew(string accessToken, TimeSpan expiresIn);
    }
}
