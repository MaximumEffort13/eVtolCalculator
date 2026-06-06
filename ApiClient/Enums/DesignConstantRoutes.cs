using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class DesignConstantRoutes : SmartEnum<DesignConstantRoutes>
{
    public static readonly DesignConstantRoutes Create = new("/api/DesignConstants/CreateDesignConstant", 0);
    public static readonly DesignConstantRoutes GetById = new("/api/DesignConstants/GetById", 1);
    public static readonly DesignConstantRoutes GetByName = new("/api/DesignConstants/GetByName", 2);
    public static readonly DesignConstantRoutes GetAll = new("/api/DesignConstants/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public DesignConstantRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
