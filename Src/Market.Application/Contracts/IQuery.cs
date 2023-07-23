using MediatR;
namespace Market.Application.Contracts;
public interface IQuery<out TResult> : IRequest<TResult>
{
}
