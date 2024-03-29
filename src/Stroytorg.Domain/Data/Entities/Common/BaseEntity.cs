﻿namespace Stroytorg.Domain.Data.Entities.Common;

public class BaseEntity<T> : IEntity, IEntity<T>
{
    public virtual required T Id { get; set; }

    public bool IsActive { get; set; } = true;


    object IEntity.Id
    {
        get => this.Id!;
        set => this.Id = (T)value;
    }
}
