namespace Contilog.Handlers.Topics
{
    public interface IGetAllTopicsHandler : IHandler<GetAllTopicsRequest, GetAllTopicsResponse>
    {
    }

    public interface IGetTopicByIdHandler : IHandler<GetTopicByIdRequest, GetTopicByIdResponse>
    {
    }

    public interface ICreateTopicHandler : IHandler<CreateTopicRequest, CreateTopicResponse>
    {
    }

    public interface IDeleteTopicHandler : IHandler<DeleteTopicRequest, DeleteTopicResponse>
    {
    }
}
