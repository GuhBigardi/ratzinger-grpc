using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using RatzingerGrpc;
using System.Threading.Tasks;

namespace RandomCodesGenerationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateRandomCodesController : ControllerBase
    {
        [HttpPost]
        public async Task<RandomCodes> Post([FromBody]GeneratorRequest generatorRequest)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Generators.GeneratorsClient(channel);
            var reply = await client.GenerateRandomCodesAsync(generatorRequest);
            return reply;
        }
    }
}
