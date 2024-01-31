using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Abstractions.Interfaces;

public interface IQuery<TResponse> : IRequest<BusinessResult<TResponse>>
{
}