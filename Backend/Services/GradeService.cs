using Backend.Contexts;
using Backend.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class GradeService : IGradeService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAGIService _agiService;



        public GradeService(DataContext context, IUserRepository userRepository, ITestRepository testRepository, IQuestionRepository questionRepository, IAGIService AGIService)
        {
            _context = context;
            _userRepository = userRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _agiService = AGIService;

        }

        public Task ApproveTest()
        {
            throw new NotImplementedException();
        }

        public async Task ProposeTestAsync(long testId,TestAnswerDTO dto)
        {
            var test = await _testRepository.GetMinimalTestAsync(testId) ?? throw new Exception($"Test not found: {testId}");
            var userId = await _userRepository.GetCurrentUserIdAsync() ?? throw new Exception("User not logged in");
            var evalDTO = await CreateEvaluationFromAnswersAsync(testId, dto);
            var agiResp = await _agiService.Evaluate(evalDTO);

            UserTestResult result = new()
            {
                UserId = userId,
                TestId = testId,
                IsFinal = false, // PROPOSAL
                TotalScore = agiResp.Questions.Sum(q => q.Grade),
            };
            _context.UserTestResults.Add(result);
            _context.SaveChanges(); // add and save to autogenerate id

            foreach (var question in agiResp.Questions)
            {
                var answerText = evalDTO.QuestionsAndAnswers
                    .Where(q => q.QuestionId == question.QuestionId)
                    .Select(q => q.AnswerText)
                    .FirstOrDefault() ?? throw new Exception($"No answer found for Question {question.QuestionId}");

                QuestionGrade grade = new()
                {
                    QuestionId = question.QuestionId,
                    GradeObtained = question.Grade,
                    Explanation = question.Explanation,
                    Answer = answerText,
                    IsApproved = false, // PROPOSAL
                    ResultId = result.ResultId,
                    UserId = userId,
                };
                _context.QuestionGrades.Add(grade);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateProposal()
        {
            throw new NotImplementedException();
        }

        private async Task<TestEvaluationDTO> CreateEvaluationFromAnswersAsync(long testId, TestAnswerDTO dto)
        {
            var t = await _testRepository.GetMinimalTestAsync(testId);
            var testEvaluationDTO = new TestEvaluationDTO
            {
                TestId = testId,
                TestDescription = t.TestDescription,
                QuestionsAndAnswers = []
            };

            foreach (var answer in dto.Answers)
            {
                var q = await _questionRepository.GetQuestionAsync(answer.QuestionId);

                var questionAndAnswer = new QuestionsAndAnswer
                {
                    QuestionId = answer.QuestionId,
                    QuestionText = q.QuestionText, 
                    AnswerText = answer.AnswerText,
                    MaxGrade = q.MaxGrade
                };

                testEvaluationDTO.QuestionsAndAnswers.Add(questionAndAnswer);
            }

            return testEvaluationDTO;
        }

    }
}
