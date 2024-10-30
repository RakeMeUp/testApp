using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IAGIService agiService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<AGIQuestionCreationResponse>> GetQuestions([FromBody] AGIQuestionCreationDTO dto)
        {
            try
            {
                return Ok(await agiService.CreateQuestion(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
