using ApiClient.DataTransferObjects;
using ApiClient.Endpoints;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace ApiClient;

public static class ServiceCollectionExtension
{
    public static void AddeVtolApiClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient("apiClient", client =>
        {
            client.BaseAddress = new Uri(config.GetRequiredSection("ApiSetting")["ApiEndPointBaseAddress"] = "https://localhost:7197");

            double timeout = Convert.ToDouble(config.GetRequiredSection("ApiSetting")["RequestTimeout"]);
            client.Timeout = TimeSpan.FromMilliseconds(timeout);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddSingleton<IAPIHelper, APIHelper>();
        services.AddSingleton<ILoggedInUserModel, LoggedInUserModel>();
    }
}
