using Microsoft.AspNetCore.Http;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using System.Security.Principal;

namespace Stroytorg.Domain.Data.Repositories;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    }

    public IPrincipal User
    {
        get
        {
            return this.contextAccessor.HttpContext?.User!;
        }
    }

    public string? GetToken()
    {
        if (this.contextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = this.contextAccessor.HttpContext.Request.Headers["Authorization"];
            string val = authHeader.First()!;

            if (val == null)
            {
                return null;
            }

            val = val.Replace("Bearer ", string.Empty);
            return val;
        }

        return null;
    }
}