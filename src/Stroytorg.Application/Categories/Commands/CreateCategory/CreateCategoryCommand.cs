using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name
    ) : IRequest<BusinessResponse<int>>;
