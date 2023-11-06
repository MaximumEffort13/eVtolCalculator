using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.ConceptualDesign;

public sealed record CreateConceptualDesignCommand(Guid UserId, ConceptualDesignInsert ConceptualDesignInsert) : ICommand<ConceptualDesignDto>;

public sealed record ConceptualDesignInsert(
    string Name,
    double TotalMassOfeVtol_kg,
    double PayloadMass_kg,
    int FlightTimeInMinutes);