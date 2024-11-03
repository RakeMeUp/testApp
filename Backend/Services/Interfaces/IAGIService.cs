using Backend.Models;
using Shared.Models;

namespace Backend.Services.Interfaces
{
    public interface IAGIService
    {
        Task<AGIEvaluateResponseDTO> Evaluate(TestEvaluationDTO dto);
        Task<AGIQuestionCreationResponse> CreateQuestion(AGIQuestionCreationDTO dto);
    }
}
