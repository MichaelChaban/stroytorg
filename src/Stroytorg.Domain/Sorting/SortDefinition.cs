namespace Stroytorg.Domain.Sorting;

public class SortDefinition
{
    public required string Field { get; set; }

    public SortDirection Direction { get; set; } = SortDirection.Asc;
}
