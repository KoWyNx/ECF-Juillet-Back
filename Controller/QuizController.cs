using ECF.Models;
using ECF.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("questions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _quizService.GetAllQuestionsAsync();
            if (questions == null) return NotFound();

            var response = questions.Select(q => new
            {
                q.QuestionId,
                q.Text,
                q.CorrectAnswer,
                QuestionOptions = q.QuestionOptions.Select(o => new
                {
                    o.OptionId,
                    o.OptionText
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("questions/{questionId}")]
        public async Task<IActionResult> GetQuestionById(int questionId)
        {
            var question = await _quizService.GetQuestionByIdAsync(questionId);
            if (question == null) return NotFound();

            var response = new
            {
                question.QuestionId,
                question.Text,
                question.CorrectAnswer,
                QuestionOptions = question.QuestionOptions.Select(o => new
                {
                    o.OptionId,
                    o.OptionText
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost("player-score")]
        public async Task<IActionResult> SubmitPlayerScore([FromBody] PlayerScore score)
        {
            try
            {
                await _quizService.SubmitPlayerScoreAsync(score);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Could not submit player score.");
            }
        }


        [HttpGet("player-scores")]
        public async Task<IActionResult> GetPlayerScores()
        {
            var scores = await _quizService.GetPlayerScoresAsync();
            if (scores == null) return NotFound();

            var response = scores.Select(s => new
            {
                s.PlayerName,
                s.Score
            }).ToList();

            return Ok(response);
        }

    }
}
