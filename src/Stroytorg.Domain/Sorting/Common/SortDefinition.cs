namespace Stroytorg.Domain.Sorting.Common;

public class SortDefinition
{
    public required string Field { get; set; }

    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}
