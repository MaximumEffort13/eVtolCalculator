using Application.Abstractions;
using Application.Commands.Person;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.AuthenticationModels;
using FluentResults;

namespace Application.Handlers.Person;
internal class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, PersonDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRespository _addressRespository;

    public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository, IAddressRespository addressRespository)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        _addressRespository = addressRespository;
    }

    public async Task<Result<PersonDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        PersonEntity person = new(Guid.NewGuid(), request.IdentityUserId, request.FirstName, request.LastName, request.PhoneNumber);

        AddressEntity address = new(Guid.NewGuid(), person.Id, request.StreetName, request.Suburb, request.City, request.Province, request.PostalCode);

        _personRepository.Create(person);
        _addressRespository.Create(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = PersonDtoMapper.Map(person, address);

        return response;
    }
}
