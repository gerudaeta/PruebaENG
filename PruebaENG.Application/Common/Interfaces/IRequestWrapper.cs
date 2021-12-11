using MediatR;
using PruebaENG.Application.Common.Wrapper;

namespace PruebaENG.Application.Common.Interfaces;

public interface IRequestWrapper<T> : IRequest<Response<T>>
{

}

public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>> where TIn : IRequestWrapper<TOut>
{

}