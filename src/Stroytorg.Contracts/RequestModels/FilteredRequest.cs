using Stroytorg.Contracts.RequestModels.Common;

namespace Stroytorg.Contracts.RequestModels;

public record FilteredRequest<TFilter> : PagedRequest
{
    public TFilter Filter { get; set; } = default!;

    public IEnumerable<OrderDescriptor>? Sort = null;

    public FilteredRequest(TFilter filter, int Limit, int Offset, IEnumerable<OrderDescriptor>? sort = null)
        : base(Limit, Offset)
    {
        Filter = filter;
        Sort = sort ?? Enumerable.Empty<OrderDescriptor>();
    }
}