using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class MotorRoutes : SmartEnum<MotorRoutes>
{
    public static readonly MotorRoutes Create = new("/api/CreateMotor", 0);
    public static readonly MotorRoutes GetById = new("/api/GetMotor", 1);
    public static readonly MotorRoutes GetByName = new("/api/GetMotor", 2);
    public static readonly MotorRoutes GetByAll = new("/api/GetAll", 3);

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
