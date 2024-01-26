using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.CreateCategory;

public record CreateCategoryCommand(
    string Name)
    : IRequest<BusinessResult<int>>;