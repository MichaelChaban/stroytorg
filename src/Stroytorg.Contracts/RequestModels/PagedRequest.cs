using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.RequestModels;

public record PagedRequest(
    [Range(0, 500)]
    int Limit = 50,

    [Range(0, int.MaxValue)]
    int Offset = 0);