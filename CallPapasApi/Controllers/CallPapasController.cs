using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using RatzingerGrpc;
using System.Threading.Tasks;

namespace CallPapasApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallPapasController : ControllerBase
    {

        [HttpGet]
        public async Task<BlessReply> Get()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Blesser.BlesserClient(channel);
            var reply = await client.SayBlessAsync(
                              new BlessRequest { Name = "Gustavo" });
            return reply;
        }
    }
}
