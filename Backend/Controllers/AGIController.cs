using AutoMapper;
using Backend.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("agi/")]
    public class AGIController(IAGIService AGIService) : ControllerBase
    {
        [HttpPost("test")]
        public async Task<ActionResult<AGIEvaluateResponseDTO>> TestAGI([FromBody] TestEvaluationDTO prompt)
        {
            var resp = await AGIService.Evaluate(prompt);
            return Ok(resp);
        }
    }
}
