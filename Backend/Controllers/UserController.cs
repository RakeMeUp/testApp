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
                            UserName = await userManager.GetUserNameAsync(user) ?? throw new Exception($"username for {userId} not found"),
                            Email = await userManager.GetEmailAsync(user) ?? throw new Exception($"email for {userId} not found")
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
                                UserName = await userManager.GetUserNameAsync(user) ?? throw new Exception($"username for {userId} not found"),
                                Email = await userManager.GetEmailAsync(user) ?? throw new Exception($"email for {userId} not found")
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
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long userId, [FromBody] UserUpdateDTO updateDto)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception($"user: {userId} not found");

                var pwsIsValid = await userManager.CheckPasswordAsync(user, updateDto.CurrentPassword);
                if (!pwsIsValid)
                {
                    throw new Exception("Invalid confirmation password given");
                }
                if (!string.IsNullOrEmpty(updateDto.UserName))
                {
                    var setUserNameResult = await userManager.SetUserNameAsync(user, updateDto.UserName);
                    if (!setUserNameResult.Succeeded)
                    {
                        throw new Exception($"Failed to update username: {updateDto.UserName}" );
                    }
                }
                if (!string.IsNullOrEmpty(updateDto.Email))
                {
                    var setEmailResult = await userManager.SetEmailAsync(user, updateDto.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        throw new Exception($"Failed to update email: {updateDto.Email}");
                    }
                }
                if (!string.IsNullOrEmpty(updateDto.Password))
                {
                    var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                    var setPasswordResult = await userManager.ResetPasswordAsync(user, resetToken, updateDto.Password);
                    if (!setPasswordResult.Succeeded)
                    {
                        throw new Exception($"Failed to update password: {updateDto.Password}");
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/delete")]
        public async Task<ActionResult> DeleteUser([FromRoute] long userId, [FromBody] UserDeleteDTO deleteDto)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception($"user: {userId} not found");
                var pwsIsValid = await userManager.CheckPasswordAsync(user, deleteDto.Password);
                if (!pwsIsValid)
                {
                    throw new Exception("Invalid confirmation password given");
                }

                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to delete user: {userId}");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
