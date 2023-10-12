using Application.Abstractions;
using Application.DTO;

namespace Application.ConceptualDesignFacilitators.Commands;

public sealed record CreateConceptualDesignCommand(double TotalMassOfeVtol, double PayloadMass, int FlightTimeInMinutes) : ICommand<ConceptualDesignDto>;

