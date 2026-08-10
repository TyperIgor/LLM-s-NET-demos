using System.Text;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;

namespace LLM.infra
{
    public class AmazonBedRockImp
    {
        private readonly AmazonBedrockRuntimeClient _client;

        public AmazonBedRockImp()
        {
            _client = new AmazonBedrockRuntimeClient();
        }

        public async Task<string> InvokeAsync(string prompt)
        {
            var body = $@"
             {{
            ""messages"": [
                {{
                    ""role"": ""user"",
                    ""content"": ""{prompt}""
                }}
            ],
            ""max_tokens"": 300
             }}";

            var request = new InvokeModelRequest
            {
                ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",
                ContentType = "application/json",
                Accept = "application/json",
                Body = new MemoryStream(Encoding.UTF8.GetBytes(body))
            };

            var response = await _client.InvokeModelAsync(request);

            using var reader = new StreamReader(response.Body);
            return await reader.ReadToEndAsync();
        }
    }
}
