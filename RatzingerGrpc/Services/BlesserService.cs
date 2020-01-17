using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RatzingerGrpc
{
    public class BlesserService : Blesser.BlesserBase
    {
        private readonly ILogger<BlesserService> _logger;
        public BlesserService(ILogger<BlesserService> logger)
        {
            _logger = logger;
        }

        public override Task<BlessReply> SayBless(BlessRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BlessReply
            {
                Message = $"Benção meu filho {request.Name}"
            });
        }
    }
}
