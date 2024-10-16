namespace Backend.Controllers
{
    using AutoMapper;
    using Backend.Entities;
    using Backend.Models;
    using Backend.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class TestsController(IMapper mapper, ITestRepository testRepository, IUserRepository userRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest([FromRoute] long id)
        {
            var test = await testRepository.GetTestAsync(id);
            return Ok(mapper.Map<TestGetDTO>(test));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Test>> GetTestsByOwnerAsync([FromRoute] long userId)
        {
            var tests = await testRepository.GetTestsByOwnerAsync(userId);
            return Ok(mapper.Map<IEnumerable<TestGetDTO>>(tests));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTest([FromBody]TestPostDTO dto)
        {
            var owner = await userRepository.GetCurrentUserAsync();
            if (owner is null)
            {
                return Unauthorized();
            }

            var testEntity = mapper.Map<Test>(dto);
            testEntity.Owner = owner;
            await testRepository.CreateTestAsync(testEntity);
            return Ok();

        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTest(long id, Test test)
        //{
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest([FromRoute]long id)
        {
            var ownerId = await userRepository.GetCurrentUserIdAsync();
            if (ownerId is null)
            {
                return Unauthorized();
            }
            var test = await testRepository.GetMinimalTestAsync(id);
            if (test.OwnerId != ownerId)
            {
                return Unauthorized();
            }
            testRepository.DeleteTest(id);
            return NoContent();

        }
    }

}
