using Stroytorg.Contracts.Enums;

namespace Stroytorg.Contracts.RequestModels.Common;

public record OrderDescriptor(
    string Field,
    SortDirection Direction = SortDirection.Ascending);
