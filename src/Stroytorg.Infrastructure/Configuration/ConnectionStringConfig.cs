using Microsoft.Extensions.Configuration;
using Stroytorg.Infrastructure.Configuration.Interfaces;

namespace Stroytorg.Infrastructure.Configuration;

public class ConnectionStringConfig : AutoBindConfig, IDatabaseConnectionString
{
    public ConnectionStringConfig(IConfiguration configuration)
        : base(configuration, "ConnectionStrings")
    {
    }

    public string ConnectionString { get; set; } = default!;
}