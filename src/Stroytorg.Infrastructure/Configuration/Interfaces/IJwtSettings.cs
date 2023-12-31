namespace Stroytorg.Infrastructure.Configuration.Interfaces;

public interface IJwtSettings
{
    string Secret { get; set; }

    double TokenExpiration { get; set; }
}
