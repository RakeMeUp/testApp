﻿namespace Backend.Controllers
{
    using AutoMapper;
    using Backend.Entities;
    using Backend.Models;
    using Backend.Repositories.Interfaces;
    using Backend.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController(IMapper mapper,IGradeService gradeService, ITestRepository testRepository, IUserRepository userRepository, IUserTestRepository userTestRepository) : ControllerBase
    {
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
        [HttpGet("owner/{userId}")]
        public async Task<ActionResult<IEnumerable<TestGetDTO>>> GetTestsByOwnerAsync([FromRoute] long userId)
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
        public async Task<ActionResult<IEnumerable<TestGetDTO>>> GetTestsByParticipationAsync([FromRoute] long userId)
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
        [HttpPost]
        public async Task<ActionResult> CreateTest([FromBody] TestPostDTO dto)
        {
            try
            {
                var user = await userRepository.GetCurrentUserAsync() ?? throw new Exception("User not logged in") ;
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
        [HttpPost("{testId}/answer")]
        public async Task<ActionResult<TestGetDTO>> AnswerTest([FromRoute] long testId, [FromBody] TestAnswerDTO dto)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (test.OwnerId == userId)
                {
                    throw new InvalidOperationException("Owner can't answer their own test");
                }
                if (!test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    throw new InvalidOperationException("User is not joined to the test");

                }
                if (test.TestResults.Where(tr=>tr.UserId == userId).Any())
                {
                    throw new InvalidOperationException("User can't answer twice, please delete existing results and answers to retry");
                }
                await gradeService.ProposeTestAsync(testId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{testId}/approve")]
        public async Task<IActionResult> ApproveTest([FromRoute] long testId, [FromBody] TestApproveDTO dto)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetMinimalTestAsync(testId);
                if (test.OwnerId != userId)
                {
                    throw new InvalidOperationException("Only the Owner can approve test evaluations");
                }
                await gradeService.ApproveTest(testId,dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{testId}/join")]
        public async Task<IActionResult> JoinTest([FromRoute]long testId)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    throw new InvalidOperationException("User have already joined to the test");
                }
                if (test.OwnerId == userId)
                {
                    throw new InvalidOperationException("Owner can not join their own test");
                }
                userTestRepository.JoinTest(testId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{testId}/leave")]
        public async Task<IActionResult> LeaveTest([FromRoute] long testId)
        {
            try
            {
                var userId = await userRepository.GetCurrentUserIdAsync();
                var test = await testRepository.GetTestAsync(testId);
                if (!test.ParticipatingUsers.Any(u => u.UserId == userId))
                {
                    throw new InvalidOperationException("User has not joined to test yet");
                }
                if (test.OwnerId == userId)
                {
                    throw new InvalidOperationException("Owner can not leave their own test");
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
