using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface IUserTestResultRepository
    {
        public Task<UserTestResult> GetResultAsync(long testId, long userId);
    }
}