using Microsoft.Extensions.Configuration;
using Stroytorg.Infrastructure.Configuration.Interfaces;

namespace Stroytorg.Infrastructure.Configuration;

public class JwtSettingsConfig : AutoBindConfig, IJwtSettings
{
    public JwtSettingsConfig(IConfiguration configuration)
        : base(configuration, "JwtSettings")
    {
    }

    public string Secret { get; set; } = default!;

    public double TokenExpiration { get; set; } = default!;
}
