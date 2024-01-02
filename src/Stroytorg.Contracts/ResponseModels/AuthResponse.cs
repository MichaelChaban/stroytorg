namespace Stroytorg.Contracts.ResponseModels;

public record AuthResponse(
    bool isLogged = false,
    string? AuthErrorMessage = null,
    JwtTokenResponse? JwtToken = null);
