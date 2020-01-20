using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using RatzingerGrpc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RandomCodesGenerationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateRandomCodesController : ControllerBase
    {
        [HttpPost]
        public async Task<string> Post([FromBody]GeneratorRequest generatorRequest)
        {
            var sw = Stopwatch.StartNew();
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Generators.GeneratorsClient(channel);
            await client.GenerateRandomCodesAsync(generatorRequest);
            return sw.ElapsedMilliseconds.ToString();
        }
    }
}
