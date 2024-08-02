using ECF.Context;
using ECF.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Question?> GetRandomQuestionAsync()
        {
            return await _context.Questions
                .Include(q => q.QuestionOptions)
                .OrderBy(q => Guid.NewGuid())  
                .FirstOrDefaultAsync();
        }

        public async Task AddPlayerScoreAsync(PlayerScore playerScore)
        {
            _context.PlayerScores.Add(playerScore);
            await _context.SaveChangesAsync();
        }
    }
}
