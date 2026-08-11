
namespace LLM.domain.Interfaces
{
    public interface IBusinessLogic
    {
        bool ApproveOrDenyCredit(int score, decimal amount);
    }
}
