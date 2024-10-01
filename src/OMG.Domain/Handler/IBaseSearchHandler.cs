using OMG.Domain.Base;

namespace OMG.Domain.Handler;


public interface IBaseSearchHandler<TypeReturn>
{
    Task<Response<IEnumerable<TypeReturn>>> GetAll(string UrlAction);
    Task<Response<IEnumerable<TypeReturn>>> GetAll(string UrlAction, string Search);
}
