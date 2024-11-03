using Shared.Models;

namespace TestApp.Client.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<AGIQuestionCreationResponse> CreateQuestions(AGIQuestionCreationDTO dto);
    }
}
