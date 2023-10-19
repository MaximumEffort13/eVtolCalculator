using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Person;
public sealed record GetPersonByUserIdQuery(Guid UserId) : IQuery<PersonDto>;

