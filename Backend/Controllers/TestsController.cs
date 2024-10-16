namespace Backend.Controllers
{
    using AutoMapper;
    using Backend.Contexts;
    using Backend.Entities;
    using Backend.Models;
    using Backend.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;

        public TestsController(IMapper mapper, ITestRepository testRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _userRepository = userRepository;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        //{

        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Test>> GetTest(long id)
        //{
        //}

        [HttpPost]
        public async Task<ActionResult> CreateTest([FromBody]TestPostDTO dto)
        {
            var owner = await _userRepository.GetCurrentUserAsync();
            if (owner is null)
            {
                return Unauthorized();
            }

            var testEntity = _mapper.Map<Test>(dto);
            testEntity.Owner = owner;
            await _testRepository.CreateTestAsync(testEntity);
            return Ok();

        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTest(long id, Test test)
        //{
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTest(long id)
        //{
        //}
    }

}
