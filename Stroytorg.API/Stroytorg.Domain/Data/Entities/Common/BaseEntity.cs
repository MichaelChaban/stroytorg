namespace Stroytorg.Domain.Data.Entities.Common;

public class BaseEntity : IEntity<int>
{
    public int Id { get; set; }

    public bool IsActive { get; set; }
}
