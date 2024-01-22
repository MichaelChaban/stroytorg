using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Users.QueryHandlers;

public class GetUserByEmailQueryHandler(
    IUserRepository userRepository,
    IAutoMapperTypeMapper autoMapperTypeMapper) : 
    IRequestHandler<GetUserByEmailQuery, BusinessResponse<User>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResponse<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.NotExistingUser,
                IsSuccess: false);
        }

        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(user));
    }
}
