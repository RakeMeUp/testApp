using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task CreateTestAsync(Test test);
    }
}
