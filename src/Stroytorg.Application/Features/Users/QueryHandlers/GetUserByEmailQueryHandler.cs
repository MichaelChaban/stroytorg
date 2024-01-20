using MediatR;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Users.QueryHandlers;

public class GetUserByEmailQueryHandler : IRequestHandler<
    GetUserByEmailQuery, BusinessResponse<User>>
{
    public Task<BusinessResponse<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
