using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Abstractions.Interfaces;

public interface ICommand : IRequest<BusinessResult>
{
}

public interface ICommand<TResponse> : IRequest<BusinessResult<TResponse>>
{
}
