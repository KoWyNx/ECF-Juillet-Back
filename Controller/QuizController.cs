using ECF.Models;
using ECF.Service;
using Microsoft.AspNetCore.Mvc;

namespace ECF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("questions/{questionId}")]
        public async Task<IActionResult> GetQuestionById(int questionId)
        {
            var question = await _quizService.GetQuestionByIdAsync(questionId);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpGet("questions/random")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            var question = await _quizService.GetRandomQuestionAsync();

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpPost("player-score")]
        public async Task<IActionResult> PostPlayerScore([FromBody] PlayerScore playerScore)
        {
            await _quizService.AddPlayerScoreAsync(playerScore);
            return CreatedAtAction(nameof(PostPlayerScore), new { id = playerScore.ScoreId }, playerScore);
        }

        [HttpGet("test")]
        public IActionResult TestEndpoint()
        {
            return Ok(new { Message = "API is working correctly!" });
        }
    }
}
