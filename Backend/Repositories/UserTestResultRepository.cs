using Backend.Contexts;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserTestResultRepository : IUserTestResultRepository
    {
        private readonly DataContext _context;
        public UserTestResultRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserTestResult> GetResultAsync(long testId, long userId)
        {
            return await _context.UserTestResults
                .Where(r => r.TestId == testId && r.UserId == userId)
                .Include(r => r.QuestionGrades)
                .FirstOrDefaultAsync() ?? throw new Exception($"Result not found for user {userId} and test {testId}");
        }
    }
}
