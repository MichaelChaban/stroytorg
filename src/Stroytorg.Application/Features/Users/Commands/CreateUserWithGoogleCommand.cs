using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Users.Commands;

public record CreateUserWithGoogleCommand(
    UserGoogleAuth User) : IRequest<BusinessResponse<User>>;
