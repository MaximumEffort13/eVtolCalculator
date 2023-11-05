using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class BladeRoutes : SmartEnum<BladeRoutes>
{
    public static readonly BladeRoutes Create = new("/api/Blade/CreateBlade", 0);
    public static readonly BladeRoutes GetById = new("/api/Blade/GetBladeById", 1);
    public static readonly BladeRoutes GetByName = new("/api/Blade/GetBladeByName", 2);
    public static readonly BladeRoutes GetAll = new("/api/Blade/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public BladeRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
