using Backend.Entities;
using Shared.Models;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, TokenService tokenService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {

            try
            {
                var user = new ApplicationUser 
                { 
                    UserName = dto.UserName, 
                    Email = dto.Email 
                };
                var result = await userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)throw new Exception(result.Errors.ToString());

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var user = await userManager.FindByNameAsync(dto.UserName) ?? throw new Exception($"user: {dto.UserName} not found");
                if (await userManager.CheckPasswordAsync(user, dto.Password))
                {
                    var token = tokenService.GenerateToken(user);
                    return Ok(
                        new TokenResponse() 
                        { 
                            Token = token 
                        }
                    );
                }
                return Unauthorized();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
    }

}
