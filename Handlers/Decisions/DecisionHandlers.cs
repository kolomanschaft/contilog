using Contilog.Models;
using Contilog.Repositories;

namespace Contilog.Handlers.Decisions
{
    public class GetDecisionsByPostIdHandler : IGetDecisionsByPostIdHandler
    {
        private readonly IDecisionRepository _decisionRepository;

        public GetDecisionsByPostIdHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<GetDecisionsByPostIdResponse> Handle(GetDecisionsByPostIdRequest request)
        {
            var decisions = await _decisionRepository.GetDecisionsByPostId(request.PostId);
            return new GetDecisionsByPostIdResponse(decisions);
        }
    }

    public class GetDecisionByIdHandler : IGetDecisionByIdHandler
    {
        private readonly IDecisionRepository _decisionRepository;

        public GetDecisionByIdHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<GetDecisionByIdResponse> Handle(GetDecisionByIdRequest request)
        {
            var decision = await _decisionRepository.GetDecisionById(request.DecisionId);
            return new GetDecisionByIdResponse(decision);
        }
    }

    public class CreateDecisionHandler : ICreateDecisionHandler
    {
        private readonly IDecisionRepository _decisionRepository;
        private readonly IPostRepository _postRepository;

        public CreateDecisionHandler(IDecisionRepository decisionRepository, IPostRepository postRepository)
        {
            _decisionRepository = decisionRepository;
            _postRepository = postRepository;
        }

        public async Task<CreateDecisionResponse> Handle(CreateDecisionRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return new CreateDecisionResponse(null, false);
            }

            // Validate that the post exists
            var post = await _postRepository.GetPostById(request.PostId);
            if (post == null)
            {
                return new CreateDecisionResponse(null, false);
            }

            // Create the decision entity
            var decision = new Decision
            {
                Text = request.Text.Trim(),
                PostId = request.PostId,
                Author = request.Author,
                CreatedDate = DateTime.UtcNow
            };

            // Save via repository
            var createdDecision = await _decisionRepository.CreateDecision(decision);
            return new CreateDecisionResponse(createdDecision, createdDecision != null);
        }
    }

    public class UpdateDecisionHandler : IUpdateDecisionHandler
    {
        private readonly IDecisionRepository _decisionRepository;

        public UpdateDecisionHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<UpdateDecisionResponse> Handle(UpdateDecisionRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return new UpdateDecisionResponse(null, false);
            }

            // Get the existing decision
            var existingDecision = await _decisionRepository.GetDecisionById(request.DecisionId);
            if (existingDecision == null)
            {
                return new UpdateDecisionResponse(null, false);
            }

            // Update the decision
            existingDecision.Text = request.Text.Trim();

            // Save via repository
            var updatedDecision = await _decisionRepository.UpdateDecision(existingDecision);
            return new UpdateDecisionResponse(updatedDecision, updatedDecision != null);
        }
    }

    public class DeleteDecisionHandler : IDeleteDecisionHandler
    {
        private readonly IDecisionRepository _decisionRepository;

        public DeleteDecisionHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<DeleteDecisionResponse> Handle(DeleteDecisionRequest request)
        {
            // Business logic: could add validation here (e.g., check permissions, soft delete, etc.)
            var success = await _decisionRepository.DeleteDecision(request.DecisionId);
            return new DeleteDecisionResponse(success);
        }
    }
}
