using Backend.Contexts;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class QuestionGradeRepository : IQuestionGradeRepository
    {
        private readonly DataContext _context;

        public QuestionGradeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionGrade>> GetGradesByResultId(long resultId)
        {
            return await _context.QuestionGrades
                .Where(qg => qg.ResultId == resultId)
                .ToListAsync();
        }
    }
}
