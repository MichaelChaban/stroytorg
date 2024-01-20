using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Users.QueryHandlers;

public class GetUserQueryHandler(IUserRepository userRepository, IAutoMapperTypeMapper autoMapperTypeMapper)
    : IRequestHandler<GetUserQuery, BusinessResponse<UserDetail>>
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<BusinessResponse<UserDetail>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(query.UserId, cancellationToken);
        if (user is null)
        {
            return new BusinessResponse<UserDetail>(
                BusinessErrorMessage: cancellationToken.IsCancellationRequested ?
                BusinessErrorMessage.OperationCancelled : BusinessErrorMessage.NotExistingUser,
                IsSuccess: false);
        }

        return new BusinessResponse<UserDetail>(
            Value: autoMapperTypeMapper.Map<UserDetail>(user));
    }
}
