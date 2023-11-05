using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class InverterRoutes : SmartEnum<InverterRoutes>
{
    public static readonly InverterRoutes Create = new("/api/Inverter/CreateInverter", 0);
    public static readonly InverterRoutes GetById = new("/api/Inverter/GetInverterById", 1);
    public static readonly InverterRoutes GetByName = new("/api/Inverter/GetInverterByName", 2);
    public static readonly InverterRoutes GetAll = new("/api/Inverter/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public InverterRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
