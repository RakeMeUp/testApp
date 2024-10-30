using AutoMapper;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<ApplicationUser> userManager, ITestRepository testRepository) : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserGetDTO>> GetUser([FromRoute] long userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception($"user: {userId} not found");
                return Ok
                    (
                        new UserGetDTO
                        {
                            UserId = userId,
                            UserName = await userManager.GetUserNameAsync(user) ?? throw new Exception($"username for {userId} not found")
                        }
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("test/{testId}")]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetParticipants([FromRoute] long testId)
        {
            try
            {
                var test = await testRepository.GetTestAsync(testId);
                var participants = test.ParticipatingUsers.Select(pu=>pu.UserId).ToList();
                List<UserGetDTO> users = [];
                foreach (var userId in participants) 
                {
                    var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception($"user: {userId} not found");
                    users.Add
                        (
                            new UserGetDTO
                            {
                                UserId = userId,
                                UserName = await userManager.GetUserNameAsync(user) ?? throw new Exception($"username for {userId} not found")
                            }
                        );
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
