using ECF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECF.Service
{
    public interface IQuizService
    {
        Task<Question?> GetQuestionByIdAsync(int questionId);
        Task<IEnumerable<Question>> GetAllQuestionsAsync(); 
        Task<bool> EvaluateAnswerAsync(int questionId, string givenAnswer);
        Task SubmitPlayerScoreAsync(PlayerScore playerScore);

        Task<List<PlayerScore>> GetPlayerScoresAsync();

    }
}
