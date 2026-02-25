namespace Huawei.DeveloperApi
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;

    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddHuaweiDeveloperApi(this IServiceCollection services, string configKey = "Huawei")
        {
            services.AddOptions<HuaweiOptions>()
                    .Configure<IConfiguration>((opts, config) => config.GetSection(configKey)?.Bind(opts))
                    .Validate(opts => opts.ClientId.HasValue(), $"{nameof(HuaweiOptions.ClientId)} is empty.")
                    .Validate(opts => opts.ClientSecret.HasValue(), $"{nameof(HuaweiOptions.ClientSecret)} is empty.");

            services.AddScoped<HuaweiDeveloperService>();

            services.AddSingleton<IHuaweiTokenStorage, HuaweiInMemoryTokenStorage>();

            return services;
        }
    }
}
