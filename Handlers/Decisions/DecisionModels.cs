using Contilog.Models;

namespace Contilog.Handlers.Decisions
{
    // Requests
    public record GetDecisionsByPostIdRequest(int PostId);
    
    public record GetDecisionByIdRequest(int DecisionId);
    
    public record CreateDecisionRequest(string Text, int PostId, string Author);
    
    public record UpdateDecisionRequest(int DecisionId, string Text);
    
    public record DeleteDecisionRequest(int DecisionId);

    // Responses
    public record GetDecisionsByPostIdResponse(IEnumerable<Decision> Decisions);
    
    public record GetDecisionByIdResponse(Decision? Decision);
    
    public record CreateDecisionResponse(Decision? Decision, bool Success);
    
    public record UpdateDecisionResponse(Decision? Decision, bool Success);
    
    public record DeleteDecisionResponse(bool Success);
}
