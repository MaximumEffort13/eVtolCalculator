using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Person;
public sealed record CreatePersonCommand(Guid IdentityUserId,
                                         string FirstName,
                                         string LastName,
                                         string Email,
                                         string PhoneNumber,
                                         string StreetName,
                                         string Suburb,
                                         string City,
                                         string Province,
                                         string PostalCode) : ICommand<PersonDto>;
