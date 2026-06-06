using Ardalis.SmartEnum;

namespace ApiClient.Enums;

public sealed class DetailDesignRoutes : SmartEnum<DetailDesignRoutes>
{
    public static readonly DetailDesignRoutes CreateNew = new("/api/DetailDesign/CreateFullNewDetailDesign", 0);
    public static readonly DetailDesignRoutes Create = new("/api/DetailDesign/CreateFullNewDetailDesignWithGuidLinks", 1);
    public static readonly DetailDesignRoutes GetById = new("/api/DetailDesign/GetDetailDesignById", 2);
    public static readonly DetailDesignRoutes GetByName = new("/api/DetailDesign/GetDetailDesignByName", 3);
    public static readonly DetailDesignRoutes GetAll = new("/api/DetailDesign/GetAll", 4);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public DetailDesignRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}
