using Backend.Contexts;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        public QuestionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Question> GetQuestionAsync(long questionId)
        {
            return await _context.Questions
                .Where(q=>q.QuestionId == questionId)
                .FirstOrDefaultAsync() ?? throw new Exception($"Question not found: {questionId}");
        }
    }
}
