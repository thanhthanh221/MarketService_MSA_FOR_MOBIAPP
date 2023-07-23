using Market.Application.Contracts;
using MediatR;

namespace Market.Application.Configurations.Queries;
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}
