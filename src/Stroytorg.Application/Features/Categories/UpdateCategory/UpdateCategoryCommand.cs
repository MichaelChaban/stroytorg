using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.UpdateCategory;

public record UpdateCategoryCommand(
    int CategoryId,
    string Name)
    : IRequest<BusinessResult<int>>;