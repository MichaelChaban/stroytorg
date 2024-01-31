using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.CreateUser;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper)
    : ICommandHandler<CreateUserCommand, User>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResult<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userToAdd = autoMapperTypeMapper.Map<Domain.Data.Entities.User>(command);

        await userRepository.AddAsync(userToAdd);
        await userRepository.UnitOfWork.CommitAsync(cancellationToken);

        var contractPerson = autoMapperTypeMapper.Map<User>(userToAdd);

        return BusinessResult.Success(contractPerson);
    }
}
