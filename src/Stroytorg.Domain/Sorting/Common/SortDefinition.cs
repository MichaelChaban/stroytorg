namespace Stroytorg.Domain.Sorting.Common;

public class SortDefinition
{
    public string? Field { get; set; }

    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}
