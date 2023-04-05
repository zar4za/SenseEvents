using MediatR;

namespace SenseEvents.Infrastructure.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
