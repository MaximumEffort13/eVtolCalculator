using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class MotorRoutes : SmartEnum<MotorRoutes>
{
    public static readonly MotorRoutes Create = new("/api/Motor/CreateMotor", 0);
    public static readonly MotorRoutes GetById = new("/api/Motor/GetMotorById", 1);
    public static readonly MotorRoutes GetByName = new("/api/Motor/GetMotorByName", 2);
    public static readonly MotorRoutes GetAll = new("/api/Motor/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public MotorRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
