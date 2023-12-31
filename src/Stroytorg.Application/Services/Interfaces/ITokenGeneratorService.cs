using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface ITokenGeneratorService
{
    JwtTokenResponse GenerateToken(User user);
}
