namespace Backend.Controllers
{
    using AutoMapper;
    using Backend.Entities;
    using Backend.Models;
    using Backend.Repositories.Interfaces;
    using Backend.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController(IMapper mapper,IGradeService gradeService, ITestRepository testRepository, IUserRepository userRepository, IUserTestRepository userTestRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateTest([FromBody] TestPostDTO dto)
        {
            try
            {
                var user = await userRepository.GetCurrentUserAsync();
                var testEntity = mapper.Map<Test>(dto);
                testEntity.Owner = user;
                await testRepository.CreateTestAsync(testEntity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{testId}")]
        public async Task<ActionResult<TestGetDTO>> GetTest([FromRoute] long testId)
        {
            try
            {
                var test = await testRepository.GetTestAsync(testId);
            return Ok(mapper.Map<TestGetDTO>(test));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{testId}/answer")]
        public async Task<ActionResult<TestGetDTO>> AnswerTest([FromRoute] long testId, [FromBody] TestAnswerDTO dto)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (test.OwnerId == userId)
                {
                    return BadRequest("Owner cant answer their own test");
                }
                if (!test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    return BadRequest("User is not joined to the test");

                }
                await gradeService.ProposeTestAsync(testId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("owner/{userId}")]
        public async Task<ActionResult<TestGetDTO>> GetTestsByOwnerAsync([FromRoute] long userId)
        {
            try
            {
                var tests = await testRepository.GetTestsByOwnerAsync(userId);
                return Ok(mapper.Map<IEnumerable<TestGetDTO>>(tests));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("participation/{userId}")]
        public async Task<ActionResult<TestGetDTO>> GetTestsByParticipationAsync([FromRoute] long userId)
        {
            try
            {
                var tests = await testRepository.GetTestsByParticipationAsync(userId);
                return Ok(mapper.Map<IEnumerable<TestGetDTO>>(tests));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("tests/{testId}/join")]
        public async Task<ActionResult> JoinTest([FromRoute]long testId)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    return BadRequest("User have already joined to the test");
                }
                if (test.OwnerId == userId)
                {
                    return BadRequest("Owner can not join their own test");
                }
                userTestRepository.JoinTest(testId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("tests/{testId}/leave")]
        public async Task<ActionResult> LeaveTest([FromRoute] long testId)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (!test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    return BadRequest("User has not joined to test yet");
                }
                if (test.OwnerId == userId)
                {
                    return BadRequest("Owner can not leave their own test");
                }
                userTestRepository.LeaveTest(testId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest([FromRoute]long id)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetMinimalTestAsync(id);
                if (test.OwnerId != userId)
                {
                    return Unauthorized();
                }
                testRepository.DeleteTest(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
