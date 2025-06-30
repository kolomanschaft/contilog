namespace Contilog.Handlers
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }

    public interface IHandler<TRequest>
    {
        Task HandleAsync(TRequest request);
    }
}
