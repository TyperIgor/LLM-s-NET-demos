using System.Text.Json;
using LLM.application.Interfaces;
using LLM.application.Models;
using LLM.domain.Interfaces;

namespace LLM.application
{
    public class LlmOrchestrator(IBedrockLLm bedrockLLm, IBusinessLogic businessLogic) : ILLMOrchestrator
    {
        private readonly IBedrockLLm _bedrockLLm = bedrockLLm;
        private readonly IBusinessLogic _businessLogic = businessLogic;


        public async Task<string> ProcessAsync(string userInput)
        {
            var llmResponse = await _bedrockLLm.InvokeAsync(userInput);

            var action = TryParseAction(llmResponse);

            if (action == null)
                return llmResponse;

            var result = ExecuteAction(action);

            // manda resultado de volta pro LLM gerar resposta final
            var finalResponse = await _bedrockLLm.InvokeAsync(
                $"Resultado da ação: {result}. Gere resposta amigável ao usuário."
            );

            return finalResponse;
        }

        private LlmAction? TryParseAction(string response)
        {
            try
            {
                return JsonSerializer.Deserialize<LlmAction>(response);
            }
            catch
            {
                return null;
            }
        }

        private string ExecuteAction(LlmAction action)
        {
            switch (action.Action)
            {
                case "approve_credit":
                    var score = Convert.ToInt32(action.Params["score"]);
                    var income = Convert.ToDecimal(action.Params["income"]);

                    var approved = _businessLogic.ApproveOrDenyCredit(score, income);

                    return approved ? "APROVADO" : "NEGADO";

                default:
                    return "Ação desconhecida";
            }
        }
    }
}
