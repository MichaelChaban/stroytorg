using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.GetUser;

public class GetUserQueryHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper) :
    IRequestHandler<GetUserQuery, BusinessResult<UserDetail>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResult<UserDetail>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(query.UserId, cancellationToken);

        return BusinessResult.Success(autoMapperTypeMapper.Map<UserDetail>(user));
    }
}
