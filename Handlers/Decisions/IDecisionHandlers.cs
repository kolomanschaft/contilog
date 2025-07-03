using Contilog.Handlers;

namespace Contilog.Handlers.Decisions
{
    public interface IGetDecisionsByPostIdHandler : IHandler<GetDecisionsByPostIdRequest, GetDecisionsByPostIdResponse>
    {
    }

    public interface IGetDecisionByIdHandler : IHandler<GetDecisionByIdRequest, GetDecisionByIdResponse>
    {
    }

    public interface ICreateDecisionHandler : IHandler<CreateDecisionRequest, CreateDecisionResponse>
    {
    }

    public interface IUpdateDecisionHandler : IHandler<UpdateDecisionRequest, UpdateDecisionResponse>
    {
    }

    public interface IDeleteDecisionHandler : IHandler<DeleteDecisionRequest, DeleteDecisionResponse>
    {
    }
}
