namespace Stroytorg.Contracts.ResponseModels;

public record AuthResponse(
    bool IsLoggedIn = false,
    string? AuthErrorMessage = null,
    JwtTokenResponse? JwtToken = null);
