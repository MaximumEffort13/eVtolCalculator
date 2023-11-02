using Ardalis.SmartEnum;

namespace ApiClient.Enums;

/// <summary>
/// A SmartEnum representing the different api request endpoints
/// </summary>
public sealed class UserRoutes : SmartEnum<UserRoutes>
{
    public static readonly UserRoutes Authenticate = new("/api/Token/authenticate", 0);
    public static readonly UserRoutes Refresh = new("/api/Token/refresh", 1);
    public static readonly UserRoutes Register = new("/api/User/register", 2);
    public static readonly UserRoutes GetAllUsers = new("/api/User/Admin/GetAllUsers", 3);
    public static readonly UserRoutes GetAllRoles = new("/api/User/Admin/GetAllRoles", 4);
    public static readonly UserRoutes AddRole = new("/api/User/Admin/AddRole", 5);
    public static readonly UserRoutes RemoveRole = new("/api/User/Admin/RemoveRole", 6);
    public static readonly UserRoutes User = new("/api/User", 7);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public UserRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
