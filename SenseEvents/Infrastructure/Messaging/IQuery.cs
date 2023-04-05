using MediatR;

namespace SenseEvents.Infrastructure.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
