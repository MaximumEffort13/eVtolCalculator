using Application.Abstractions;
using Application.DTO;

namespace Application.ConceptualDesignFacilitators.Commands;

public sealed record CreateConceptualDesignCommand(
    double TotalMassOfeVtol_kg,
    double PayloadMass_kg,
    int FlightTimeInMinutes) : ICommand<ConceptualDesignDto>;

