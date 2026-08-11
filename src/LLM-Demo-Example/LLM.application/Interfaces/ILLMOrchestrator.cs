namespace LLM.application.Interfaces
{
    public interface ILLMOrchestrator
    {
        Task<string> ProcessAsync(string userInput);
    }
}
