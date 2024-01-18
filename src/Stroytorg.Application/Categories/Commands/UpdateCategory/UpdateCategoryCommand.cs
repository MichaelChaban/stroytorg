using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    int CategoryId,
    string Name
    ) : IRequest<BusinessResponse<int>>;
