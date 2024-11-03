using Shared.Models;

namespace TestApp.Client.Services.Interfaces
{
    public interface ITestService
    {
        Task<TestGetDTO> GetAsync(long id);
        Task<IEnumerable<TestGetDTO>> GetByOwnerAsync(long ownerId);
        Task<IEnumerable<TestGetDTO>> GetByParticipationAsync(long ownerId);
        Task<HttpResponseMessage> CreateTest(TestPostDTO post);
        Task<HttpResponseMessage> DeleteTest(long id);
        Task<HttpResponseMessage> AnswerTest(long id, TestAnswerDTO answers);
        Task<HttpResponseMessage> ApproveTest(long id, TestApproveDTO answers);
        Task<HttpResponseMessage> JoinTest(long id);
        Task<HttpResponseMessage> LeaveTest(long id);
    }
}
