using Application.Abstractions;
using Application.Commands.DesignConstants;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using FluentResults;

namespace Application.Handlers.DesignConstants
{
    internal class CreateDesignConstantCommandHandler(IDesignConstantsRepository designConstants, IUnitOfWork unitOfWork) : ICommandHandler<CreateDesignConstantsCommand, DesignConsantsDto>
    {
        private readonly IDesignConstantsRepository _designConstants = designConstants;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<DesignConsantsDto>> Handle(CreateDesignConstantsCommand request, CancellationToken cancellationToken)
        {
            bool isUniqueName = await _designConstants.IsNameUnique(request.Name);

            if (isUniqueName is false)
            {
                return Result.Fail<DesignConsantsDto>("Name should be unique");
            }

            var desingConst = new DesignConstantsEntity(Guid.NewGuid(), request.Name, request.Value);

            _designConstants.Create(desingConst);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var designConstsDto = DesignConstantsDtoMapper.Map(desingConst);

            return designConstsDto;
        }
    }
}
