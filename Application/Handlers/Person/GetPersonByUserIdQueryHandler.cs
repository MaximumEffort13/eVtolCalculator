using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Person;
using Domain.Abstractions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Person;
internal class GetPersonByUserIdQueryHandler : IQueryHandler<GetPersonByUserIdQuery, PersonDto>
{
    private readonly IPersonRepository _personRepo;
    private readonly IAddressRespository _addressRespository;

    public GetPersonByUserIdQueryHandler(IPersonRepository personRepo, IAddressRespository addressRespository)
    {
        _personRepo = personRepo;
        _addressRespository = addressRespository;
    }

    public async Task<Result<PersonDto>> Handle(GetPersonByUserIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepo.GetByUserIdAsync(request.UserId, cancellationToken);

        if (person is null)
        {
            return Result.Fail("Could not find user");
        }

        var address = await _addressRespository.GetByPersonIdAsync(person.Id, cancellationToken);
        if (address is null)
        {
            return Result.Fail("Could not find appropriate address for user.");
        }

        var response = PersonDtoMapper.Map(person, address);

        return response;
    }
}
