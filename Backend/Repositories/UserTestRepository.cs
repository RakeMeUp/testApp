using Backend.Contexts;
using Backend.Entities.Joins;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;
        private readonly DataContext _dataContext;
        public UserTestRepository(IUserRepository userRepository, ITestRepository testRepository, DataContext dataContext) {
            _userRepository = userRepository;
            _testRepository = testRepository;
            _dataContext = dataContext;
        }
        public async void JoinTest(long testId)
        {
            var userId = await _userRepository.GetCurrentUserIdAsync() ?? throw new Exception("User not logged in");
            var participation = new UserParticipatedTest
            {
                UserId = userId,
                TestId = testId
            };
            _dataContext.UserParticipatedTests.Add(participation);
            await _dataContext.SaveChangesAsync();
        }

        public async void LeaveTest(long testId)
        {
            var userId = await _userRepository.GetCurrentUserIdAsync() ?? throw new Exception("User not logged in");
            var participation = await _dataContext.UserParticipatedTests
                .FirstOrDefaultAsync(up => up.UserId == userId && up.TestId == testId) ?? throw new Exception($"User is currently not in test: {testId}");
            _dataContext.UserParticipatedTests.Remove(participation);
            await _dataContext.SaveChangesAsync();
        }

    }
}
