namespace Stroytorg.Domain.Data.Entities.Common;

public interface IEntity<T>
{
    T Id { get; set; }
}
