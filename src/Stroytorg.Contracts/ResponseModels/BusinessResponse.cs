namespace Stroytorg.Contracts.ResponseModels;

public record BusinessResponse<T>(
    T Value = default!,
    string? BusinessErrorMessage = null,
    bool IsSuccess = true);