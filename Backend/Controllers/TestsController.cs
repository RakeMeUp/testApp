namespace Backend.Controllers
{
    using AutoMapper;
    using Backend.Entities;
    using Backend.Models;
    using Backend.Repositories;
    using Backend.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class TestsController(IMapper mapper, ITestRepository testRepository, IUserRepository userRepository, IUserTestRepository userTestRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<TestGetDTO>> GetTest([FromRoute] long id)
        {
            var test = await testRepository.GetTestAsync(id);
            return Ok(mapper.Map<TestGetDTO>(test));
        }

        [HttpGet("owner/{userId}")]
        public async Task<ActionResult<TestGetDTO>> GetTestsByOwnerAsync([FromRoute] long userId)
        {
            var tests = await testRepository.GetTestsByOwnerAsync(userId);
            return Ok(mapper.Map<IEnumerable<TestGetDTO>>(tests));
        }

        [HttpGet("participation/{userId}")]
        public async Task<ActionResult<TestGetDTO>> GetTestsByParticipationAsync([FromRoute] long userId)
        {
            var tests = await testRepository.GetTestsByParticipationAsync(userId);
            return Ok(mapper.Map<IEnumerable<TestGetDTO>>(tests));
        }

        [HttpPost("tests/{testId}/join")]
        public ActionResult JoinTest([FromRoute]long testId)
        {
            userTestRepository.JoinTest(testId);
            return NoContent();
        }
        [HttpPost("tests/{testId}/leave")]
        public ActionResult LeaveTest([FromRoute] long testId)
        {
            userTestRepository.LeaveTest(testId);
            return NoContent();
        }

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
