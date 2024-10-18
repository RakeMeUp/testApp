using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IAGIService
    {
        Task<AGIEvaluateResponseDTO> Evaluate(TestEvaluationDTO dto);
    }
}
