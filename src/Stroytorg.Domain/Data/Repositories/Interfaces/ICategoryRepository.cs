﻿using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category, int>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken);
}
