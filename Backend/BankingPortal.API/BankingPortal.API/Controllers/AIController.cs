using BankingPortal.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly GeminiService _geminiService;

        public AIController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] string question)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(question))
                {
                    return BadRequest("Question is required.");
                }

                var answer =
                    await _geminiService.AskGeminiAsync(question);

                return Ok(new
                {
                    question = question,
                    answer = answer
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Gemini request failed.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }
    }
}