using Microsoft.Extensions.Configuration;
using Stroytorg.Infrastructure.Configuration;
using Stroytorg.Infrastructure.Infrastructure.Common;

namespace Stroytorg.Infrastructure.Infrastructure;

public class ConnectionStringConfig : AutoBindConfig, IDatabaseConnectionString
{
    public ConnectionStringConfig(IConfiguration configuration)
        : base(configuration, "ConnectionStrings")
    {
    }

    public string ConnectionString { get; set; } = default!;
}