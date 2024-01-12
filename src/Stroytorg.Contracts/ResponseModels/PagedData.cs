namespace Stroytorg.Contracts.ResponseModels;

public record PagedData<T>(
    int Total,
    IEnumerable<T> Data);
