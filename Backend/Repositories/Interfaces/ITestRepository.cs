using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task CreateTestAsync(Test test);
        Task<Test> GetTestAsync(long testId);
        Task<Test> GetMinimalTestAsync(long testId);
        Task<IEnumerable<Test?>> GetTestsByOwnerAsync(long ownerId);
        void DeleteTest(long testId);

        //TODO UPDATE
    }
}
