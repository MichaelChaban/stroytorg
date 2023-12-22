using Microsoft.Extensions.Configuration;

namespace Stroytorg.Infrastructure.Configuration;

public class AutoBindConfig
{
    public AutoBindConfig(IConfiguration configuration, string configurationName)
    {
        configuration.GetSection(configurationName).Bind(this);
    }
}
