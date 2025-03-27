using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {

    }

    public interface IQueryList<out TResponse> : IRequest<IEnumerable<TResponse>> where TResponse : notnull
    {
    }

    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
        where TResponse : notnull
    {
    }

    public interface IQueryListHandler<in TRequest, TResponse> : IRequestHandler<TRequest, IEnumerable<TResponse>>
        where TRequest : IQueryList<TResponse>
        where TResponse : notnull
    {
    }
}
