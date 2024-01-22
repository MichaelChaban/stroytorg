using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Users.Commands;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Users.CommandHandlers;

public class CreateUserWithGoogleCommandHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper) :
    IRequestHandler<CreateUserWithGoogleCommand, BusinessResponse<User>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResponse<User>> Handle(CreateUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var entityUser = await userRepository.GetByEmailAsync(request.User.Email);
        if (entityUser is not null)
        {
            return new BusinessResponse<User>(
            BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingUser,
            IsSuccess: false);
        }

        var userToAdd = autoMapperTypeMapper.Map<Domain.Data.Entities.User>(request.User);

        await userRepository.AddAsync(userToAdd);
        await userRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(userToAdd));
    }
}
