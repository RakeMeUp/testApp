using Backend.Contexts;
using Backend.Entities;
using Backend.Entities.Joins;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        public TestRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            var userCreatedTest = new UserCreatedTest
            {
                UserId = test.OwnerId,
                TestId = test.TestId
            };
            _context.UserCreatedTests.Add(userCreatedTest);
            await _context.SaveChangesAsync();
        }

        public void DeleteTest(long testId)
        {
            var test = _context.Tests
                .Include(t => t.CreatedByUsers)
                .FirstOrDefault(t => t.TestId == testId) ?? throw new Exception($"Test not found: {testId}");

            foreach (var t in test.CreatedByUsers.ToList())
            {
                _context.UserCreatedTests.Remove(t);
            }
            _context.Tests.Remove(test);
            _context.SaveChanges();
        }

        public async Task<Test> GetTestAsync(long testId)
        {
            return await _context.Tests
                .Where(t=>t.TestId == testId)
                .Include(t => t.Questions)
                .ThenInclude(t=>t.QuestionGrades)
                .Include(t=> t.ParticipatingUsers)
                .Include(t=> t.TestResults)
                .FirstOrDefaultAsync() ?? throw new Exception($"Test not found: {testId}");
        }
        public async Task<Test> GetMinimalTestAsync(long testId)
        {
            return await _context.Tests
                .Where(t => t.TestId == testId)
                .FirstOrDefaultAsync() ?? throw new Exception($"Test not found: {testId}");
        }

        public async Task<IEnumerable<Test?>> GetTestsByOwnerAsync(long ownerId)
        {
            return await _context.Tests
                .Where(t => t.OwnerId == ownerId)
                .Include(t => t.Questions)
                .ThenInclude(t => t.QuestionGrades)
                .Include(t => t.ParticipatingUsers)
                .Include(t => t.TestResults)
                .ToListAsync();
        }

        public async Task<IEnumerable<Test?>> GetTestsByParticipationAsync(long userId)
        {
            return await _context.UserParticipatedTests
                .Where(up => up.UserId == userId)
                .Include(up => up.Test)
                    .ThenInclude(t=>t.TestResults.Where(tr=>tr.UserId == userId))
                .Select(up => up.Test)
                .ToListAsync();
        }
    }
}
