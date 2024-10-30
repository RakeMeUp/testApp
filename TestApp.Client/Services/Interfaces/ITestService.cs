using Shared.Models;

namespace TestApp.Client.Services.Interfaces
{
    public interface ITestService
    {
        Task<TestGetDTO> GetAsync(long id);
        Task<IEnumerable<TestGetDTO>> GetByOwnerAsync(long ownerId);
        Task<IEnumerable<TestGetDTO>> GetByParticipationAsync(long ownerId);
        void CreateTest(TestPostDTO post);
        void DeleteTest(long id);
        Task<HttpResponseMessage> AnswerTest(long id, TestAnswerDTO answers);
        void ApproveTest(long id, TestApproveDTO answers);
        void JoinTest(long id);
        void LeaveTest(long id);
    }
}
