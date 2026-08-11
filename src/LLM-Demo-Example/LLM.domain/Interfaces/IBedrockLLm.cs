
namespace LLM.domain.Interfaces
{
    public interface IBedrockLLm
    {
        Task<string> InvokeAsync(string input);
    }
}
