using Backend.Models;
using Shared.Models;

namespace Backend.Services.Interfaces
{
    public interface IGradeService
    {
        Task ProposeTestAsync(long id, TestAnswerDTO dto);
        Task UpdateProposal();
        Task ApproveTest(long testId, TestApproveDTO dto);
    }
}
