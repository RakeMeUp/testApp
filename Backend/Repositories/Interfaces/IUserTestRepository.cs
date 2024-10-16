namespace Backend.Repositories.Interfaces
{
    public interface IUserTestRepository
    {
        void JoinTest(long testId);
        void LeaveTest(long testId);
    }
}
