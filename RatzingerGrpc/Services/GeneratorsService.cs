using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RatzingerGrpc.Services;
using System.Threading.Tasks;

namespace RatzingerGrpc
{
    public class GeneratorsService : Generators.GeneratorsBase
    {
        public override async Task<Empty> GenerateRandomCodes(GeneratorRequest request, ServerCallContext context)
        {
            var bergoglioService = new BergoglioService();
            await bergoglioService.GenerateCodes(request.QuantityPerSerie, request.SerialNumberInit, request.SerialNumberFinal);
            return new Empty();
        }
    }
}
