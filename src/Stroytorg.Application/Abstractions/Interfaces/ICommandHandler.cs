using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Abstractions.Interfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, BusinessResult>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, BusinessResult<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
