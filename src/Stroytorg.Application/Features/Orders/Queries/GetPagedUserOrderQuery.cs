using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Contracts.Sorting;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Application.Features.Orders.Queries;

public record GetPagedUserOrderQuery<TFilter>(
    TFilter? Filter,
    SortDefinition Sort = default!,

    [Range(0, 500)]
    int Offset = 0,

    [Range(0, int.MaxValue)]
    int Limit = 50
    ) : IRequest<PagedData<Order>>
    where TFilter : OrderFilter;