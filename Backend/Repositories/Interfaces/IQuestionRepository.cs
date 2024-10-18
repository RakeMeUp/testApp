using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Question> GetQuestionAsync(long questionId);
    }
}
