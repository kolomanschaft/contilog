using Contilog.Models;
using Contilog.Repositories;

namespace Contilog.Handlers.Posts
{
    public class GetPostsByTopicIdHandler : IGetPostsByTopicIdHandler
    {
        private readonly IPostRepository _postRepository;

        public GetPostsByTopicIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<GetPostsByTopicIdResponse> Handle(GetPostsByTopicIdRequest request)
        {
            var posts = await _postRepository.GetPostsByTopicIdAsync(request.TopicId);
            return new GetPostsByTopicIdResponse(posts);
        }
    }

    public class GetPostByIdHandler : IGetPostByIdHandler
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<GetPostByIdResponse> Handle(GetPostByIdRequest request)
        {
            var post = await _postRepository.GetPostByIdAsync(request.PostId);
            return new GetPostByIdResponse(post);
        }
    }

    public class GetPostCountByTopicIdHandler : IGetPostCountByTopicIdHandler
    {
        private readonly IPostRepository _postRepository;

        public GetPostCountByTopicIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<GetPostCountByTopicIdResponse> Handle(GetPostCountByTopicIdRequest request)
        {
            var count = await _postRepository.GetPostCountByTopicIdAsync(request.TopicId);
            return new GetPostCountByTopicIdResponse(count);
        }
    }

    public class CreatePostHandler : ICreatePostHandler
    {
        private readonly IPostRepository _postRepository;

        public CreatePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<CreatePostResponse> Handle(CreatePostRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return new CreatePostResponse(null, false);
            }

            // Create the post entity
            var post = new Post
            {
                Content = request.Content.Trim(),
                TopicId = request.TopicId,
                Author = request.Author,
                CreatedDate = DateTime.UtcNow
            };

            // Save via repository
            var createdPost = await _postRepository.CreatePostAsync(post);
            return new CreatePostResponse(createdPost, createdPost != null);
        }
    }

    public class UpdatePostHandler : IUpdatePostHandler
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<UpdatePostResponse> Handle(UpdatePostRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return new UpdatePostResponse(null, false);
            }

            // Get the existing post
            var existingPost = await _postRepository.GetPostByIdAsync(request.PostId);
            if (existingPost == null)
            {
                return new UpdatePostResponse(null, false);
            }

            // Update the post
            existingPost.Content = request.Content.Trim();
            existingPost.ModifiedDate = DateTime.UtcNow;

            // Save via repository
            var updatedPost = await _postRepository.UpdatePostAsync(existingPost);
            return new UpdatePostResponse(updatedPost, updatedPost != null);
        }
    }

    public class DeletePostHandler : IDeletePostHandler
    {
        private readonly IPostRepository _postRepository;

        public DeletePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<DeletePostResponse> Handle(DeletePostRequest request)
        {
            // Business logic: could add validation here (e.g., check permissions, soft delete, etc.)
            var success = await _postRepository.DeletePostAsync(request.PostId);
            return new DeletePostResponse(success);
        }
    }
}
