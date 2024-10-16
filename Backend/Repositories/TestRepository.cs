using Backend.Contexts;
using Backend.Entities;
using Backend.Entities.Joins;
using Backend.Repositories.Interfaces;

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
    }
}
