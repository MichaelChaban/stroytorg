using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Commands;

public record UpdateCategoryCommand(
    int CategoryId,
    string Name
    ) : IRequest<BusinessResponse<int>>;
