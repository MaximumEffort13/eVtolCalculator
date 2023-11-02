using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class BatteryPackRoutes : SmartEnum<BatteryPackRoutes>
{
    public static readonly BatteryPackRoutes Create = new("/api/CreateBatteryPack", 0);
    public static readonly BatteryPackRoutes GetById = new("/api/GetBatteryPack", 1);
    public static readonly BatteryPackRoutes GetByName = new("/api/GetBatteryPack", 2);
    public static readonly BatteryPackRoutes GetByAll = new("/api/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public BatteryPackRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}