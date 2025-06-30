namespace Contilog.Handlers.Topics
{
    public interface IGetAllTopicsHandler : IHandler<GetAllTopicsRequest, GetAllTopicsResponse>
    {
    }

    public interface ICreateTopicHandler : IHandler<CreateTopicRequest, CreateTopicResponse>
    {
    }

    public interface IArchiveTopicHandler : IHandler<ArchiveTopicRequest, ArchiveTopicResponse>
    {
    }

    public interface IDeleteTopicHandler : IHandler<DeleteTopicRequest, DeleteTopicResponse>
    {
    }
}
