using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.CreateUserWithGoogle;

public class CreateUserWithGoogleCommandHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper) :
    IRequestHandler<CreateUserWithGoogleCommand, BusinessResult<User>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResult<User>> Handle(CreateUserWithGoogleCommand command, CancellationToken cancellationToken)
    {
        var userToAdd = autoMapperTypeMapper.Map<Domain.Data.Entities.User>(command);

        await userRepository.AddAsync(userToAdd);
        await userRepository.UnitOfWork.CommitAsync();

        return BusinessResult.Success(autoMapperTypeMapper.Map<User>(userToAdd));
    }
}
