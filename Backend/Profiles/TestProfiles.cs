namespace Backend.Profiles
{
    using AutoMapper;
    using Backend.Models;
    using Backend.Entities;

    public class TestProfiles : Profile
    {
        public TestProfiles()
        {
            CreateMap<TestPostDTO, Test>()
                .ForMember(dest => dest.TestId, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.TestResults, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

            CreateMap<QuestionPostDTO, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.TestId, opt => opt.Ignore())
                .ForMember(dest => dest.Test, opt => opt.Ignore())
                .ForMember(dest => dest.QuestionGrade, opt => opt.Ignore());

            CreateMap<Test, TestPostDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<Test, TestGetDTO>()
                .ForMember(dest => dest.ParticipatedUserIDs, opt => opt.MapFrom(src =>
                    src.ParticipatingUsers.Select(u => u.UserId).ToList()));
            CreateMap<QuestionGrade, QuestionGradeGetDTO>();
            CreateMap<UserTestResult, UserTestResultGetDTO>();
            CreateMap<Question, QuestionGetDTO>();
            CreateMap<Question, QuestionPostDTO>();
        }
    }

}
