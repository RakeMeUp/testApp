using Backend.Contexts;
using Backend.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.Services
{
    public class GradeService : IGradeService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionGradeRepository _questionGradeRepository;
        private readonly IUserTestResultRepository _userTestResultRepository;
        private readonly IAGIService _agiService;
        public GradeService(DataContext context, IUserTestResultRepository userTestResultRepository,IQuestionGradeRepository questionGradeRepository, IUserRepository userRepository, ITestRepository testRepository, IQuestionRepository questionRepository, IAGIService AGIService)
        {
            _context = context;
            _userRepository = userRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _agiService = AGIService;
            _userTestResultRepository = userTestResultRepository;
            _questionGradeRepository = questionGradeRepository;

        }
        public async Task ApproveTest(long testId, TestApproveDTO dto)
        {
            var userId = await _userRepository.GetCurrentUserIdAsync() ?? throw new Exception("User not logged in");
            var result = await _userTestResultRepository.GetResultAsync(testId, dto.UserId);
            var grades = await _questionGradeRepository.GetGradesByResultId(result.ResultId);

            foreach (var grade in grades)
            {
                var edit = dto.GradeEdits.Where(e=>e.QuestionId == grade.QuestionId).FirstOrDefault();
                if (grade.IsApproved) continue;
                if (edit != null)
                {
                    grade.GradeObtained = edit.CorrectedGrade;
                    grade.IsApproved = true;
                    grade.ApprovedAt = DateTime.Now;
                    if(edit.Explanation != null)
                    {
                        grade.Explanation = edit.Explanation;
                    }
                }
                else
                {
                    grade.IsApproved = true;
                    grade.ApprovedAt = DateTime.Now;
                }
            }
            
            await _context.SaveChangesAsync();
            await ApproveResult(testId, dto.UserId);
        }
        private async Task ApproveResult(long testId, long userId)
        {
            var result = await _userTestResultRepository.GetResultAsync(testId, userId);
            if (result.QuestionGrades.All(qg => qg.IsApproved))
            {
                result.IsFinal = true;
                result.TotalScore = result.QuestionGrades.Sum(g => g.GradeObtained);
                await _context.SaveChangesAsync();
            }

        }
        public async Task ProposeTestAsync(long testId,TestAnswerDTO dto)
        {
            var test = await _testRepository.GetMinimalTestAsync(testId) ?? throw new Exception($"Test not found: {testId}");
            var userId = await _userRepository.GetCurrentUserIdAsync() ?? throw new Exception("User not logged in");
            var evalDTO = await CreateEvaluationDTOFromAnswersAsync(testId, dto);
            var agiResp = await _agiService.Evaluate(evalDTO);

            UserTestResult result = new()
            {
                UserId = userId,
                TestId = testId,
                IsFinal = false, // PROPOSAL
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
            result.TotalScore = result.QuestionGrades.Sum(g=>g.GradeObtained);
            _context.SaveChanges();
        }
        public Task UpdateProposal()
        {
            throw new NotImplementedException();
        }
        private async Task<TestEvaluationDTO> CreateEvaluationDTOFromAnswersAsync(long testId, TestAnswerDTO dto)
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
