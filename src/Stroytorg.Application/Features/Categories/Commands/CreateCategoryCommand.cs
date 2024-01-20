using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Commands;

public record CreateCategoryCommand(
    string Name
    ) : IRequest<BusinessResponse<int>>;
