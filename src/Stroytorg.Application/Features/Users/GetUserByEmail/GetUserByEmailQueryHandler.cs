using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.GetUserByEmail;

public class GetUserByEmailQueryHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper)
    : IRequestHandler<GetUserByEmailQuery, BusinessResult<User>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResult<User>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(query.Email, cancellationToken);
        return BusinessResult.Success(autoMapperTypeMapper.Map<User>(user!));
    }
}
