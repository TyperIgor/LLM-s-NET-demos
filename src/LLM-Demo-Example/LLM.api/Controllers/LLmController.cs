using LLM.application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LLM.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LLmController(ILLMOrchestrator orchestrator) : ControllerBase
    {
        private readonly ILLMOrchestrator _orchestrator = orchestrator;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string input)
        {
            var result = await _orchestrator.ProcessAsync(input);
            return Ok(result);
        }
    }
}
