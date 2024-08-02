using System.Threading.Tasks;
using ECF.Models;

namespace ECF.Service
{


    public interface IQuizService
    {
        Task<Question?> GetQuestionByIdAsync(int questionId);
        Task<Question?> GetRandomQuestionAsync();
        Task AddPlayerScoreAsync(PlayerScore playerScore);
    }

}
