namespace Backend.Profiles
{
    using AutoMapper;
    using Backend.Models;
    using Backend.Entities;

    public class TestProfiles : Profile
    {
        public TestProfiles()
        {
            // Map TestPostDTO to Test entity
            CreateMap<TestPostDTO, Test>()
                .ForMember(dest => dest.TestId, opt => opt.Ignore()) // TestId should be auto-generated
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore()) // OwnerId comes from context or service
                .ForMember(dest => dest.Owner, opt => opt.Ignore()) // Owner will be handled elsewhere
                .ForMember(dest => dest.TestResults, opt => opt.Ignore()) // TestResults not mapped here
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

            // Map QuestionPostDTO to Question entity
            CreateMap<QuestionPostDTO, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore()) // QuestionId should be auto-generated
                .ForMember(dest => dest.TestId, opt => opt.Ignore()) // TestId comes from context or parent object
                .ForMember(dest => dest.Test, opt => opt.Ignore()) // Test reference should be set elsewhere
                .ForMember(dest => dest.QuestionGrade, opt => opt.Ignore()); // QuestionGrade handled separately

            // Optional: Reverse mappings if needed
            CreateMap<Test, TestPostDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<Test, TestGetDTO>();
            CreateMap<Question, QuestionGetDTO>();
            CreateMap<QuestionGrade, QuestionGradeGetDTO>();
            CreateMap<UserTestResult, UserTestResultGetDTO>();

            CreateMap<Question, QuestionPostDTO>();
        }
    }

}
