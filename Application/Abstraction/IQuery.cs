using Application.Wrapper;
using MediatR;
namespace Application.Abstraction
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
