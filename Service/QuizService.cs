using ECF.Context;
using ECF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECF.Service
{
    public class QuizService : IQuizService
    {
        private readonly MyDbContext _context;

        public QuizService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions
                .Include(q => q.QuestionOptions)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.QuestionOptions)
                .ToListAsync();
        }

        public async Task<bool> EvaluateAnswerAsync(int questionId, string givenAnswer)
        {
            var question = await GetQuestionByIdAsync(questionId);
            if (question != null)
            {
                return question.CorrectAnswer.Equals(givenAnswer, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public async Task SubmitPlayerScoreAsync(PlayerScore playerScore)
        {
            _context.PlayerScores.Add(playerScore);
            await _context.SaveChangesAsync();
        }


        public async Task<List<PlayerScore>> GetPlayerScoresAsync()
        {
            return await _context.PlayerScores.ToListAsync();
        }

    }
}
