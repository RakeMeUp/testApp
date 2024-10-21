using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface IQuestionGradeRepository
    {
        public Task<IEnumerable<QuestionGrade>> GetGradesByResultId(long resultId);
    }
}
