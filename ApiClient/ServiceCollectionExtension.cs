using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.IdentityObjects;
using ApiClient.Endpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Extensions.Http;
using Polly;
using System.Net;
using System.Net.Http.Headers;

namespace ApiClient;

public static class ServiceCollectionExtension
{
    private static IAsyncPolicy<HttpResponseMessage> RetryPolicy => HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .Or<Exception>()
    .Or<HttpRequestException>()
    .Or<HttpListenerException>()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public static void AddeVtolApiClient(this IServiceCollection services, IConfiguration config)
    {
        string url = config.GetRequiredSection("ApiSetting")["ApiEndPointBaseAddress"]!;
        var timeoutSetting = config.GetRequiredSection("ApiSetting")["RequestTimeout"];

        services.AddHttpClient("apiClient", client =>
        {
            client.BaseAddress = new Uri(url = "https://localhost:7197");

            double timeout = Convert.ToDouble(timeoutSetting);
            client.Timeout = TimeSpan.FromMilliseconds(timeout);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddPolicyHandler(RetryPolicy);

        services.AddTransient<IIdentityEndpoints, IdentityEndpoints>();
        services.AddScoped<IApiHelper, ApiHelper>();
        services.AddSingleton<ILoggedInUserModel, LoggedInUserModel>();
        services.AddTransient<IUserEndpoints, UserEndpoints>();
        services.AddTransient<IBatteryPackEndpoints, BatteryPackEndpoints>();
        services.AddTransient<IBladeEndpoints,  BladeEndpoints>();
        services.AddTransient<IConceptualDesignEndpoints, ConceptualDesignEndpoints>();
        services.AddTransient<IDetailDesignEndpoints, DetailDesignEndpoints>();
        services.AddTransient<IInverterEndpoints,  InverterEndpoints>();
        services.AddTransient<IMotorEndpoints,  MotorEndpoints>();
        services.AddTransient<IDesignConstantsEndpoints, DesignConstantsEndpoints>();

        services.AddScoped<Utilities, Utilities>();
    }
}