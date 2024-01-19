using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(
    int CategoryId
    ) : IRequest<BusinessResponse<int>>;
