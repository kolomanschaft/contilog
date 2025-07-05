using Contilog.Handlers;

namespace Contilog.Handlers.Posts
{
    public interface IGetAllPostsHandler : IHandler<GetAllPostsRequest, GetAllPostsResponse>
    {
    }
    
    public interface IGetPostsByTopicIdHandler : IHandler<GetPostsByTopicIdRequest, GetPostsByTopicIdResponse>
    {
    }

    public interface IGetPostByIdHandler : IHandler<GetPostByIdRequest, GetPostByIdResponse>
    {
    }

    public interface IGetPostCountByTopicIdHandler : IHandler<GetPostCountByTopicIdRequest, GetPostCountByTopicIdResponse>
    {
    }

    public interface ICreatePostHandler : IHandler<CreatePostRequest, CreatePostResponse>
    {
    }

    public interface IUpdatePostHandler : IHandler<UpdatePostRequest, UpdatePostResponse>
    {
    }

    public interface IDeletePostHandler : IHandler<DeletePostRequest, DeletePostResponse>
    {
    }
}
