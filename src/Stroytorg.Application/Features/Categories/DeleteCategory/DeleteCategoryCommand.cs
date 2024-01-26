using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(
    int CategoryId)
    : IRequest<BusinessResult<int>>;
