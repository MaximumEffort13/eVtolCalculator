using Ardalis.SmartEnum;

namespace ApiClient.Enums;

public sealed class ConceptualDesignRoutes : SmartEnum<ConceptualDesignRoutes>
{
    public static readonly ConceptualDesignRoutes Create = new("/api/ConceptualDesign/CreateConceptualDesign", 0);
    public static readonly ConceptualDesignRoutes GetById = new("/api/ConceptualDesign/GetConceptualDesignById", 1);
    public static readonly ConceptualDesignRoutes GetByName = new("/api/ConceptualDesign/GetConceptualDesignByName", 2);
    public static readonly ConceptualDesignRoutes GetAll = new("/api/ConceptualDesign/GetAll", 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServerRequestEndpoint"/> class.
    /// </summary>
    /// <param name="endpointName">The Endpoint name.</param>
    /// <param name="id">The enum ID to use.</param>
    public ConceptualDesignRoutes(string endpointName, int id)
        : base(endpointName, id)
    {
    }
}