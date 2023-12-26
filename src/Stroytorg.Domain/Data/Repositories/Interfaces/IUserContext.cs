using System.Security.Principal;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IUserContext
{
    public IPrincipal User { get; }

    public string? GetToken();
}
