using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Commands;

public record DeleteCategoryCommand(
    int CategoryId) : IRequest<BusinessResponse<int>>;
