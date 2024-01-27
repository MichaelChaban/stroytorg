using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Abstractions.Interfaces;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, BusinessResult<TResponse>>
    where TQuery : IQuery<TResponse>
{
}