using Grpc.Core;
using RatzingerGrpc.Services;
using System.Threading.Tasks;

namespace RatzingerGrpc
{
    public class GeneratorsService : Generators.GeneratorsBase
    {
        public override async Task<RandomCodes> GenerateRandomCodes(GeneratorRequest request, ServerCallContext context)
        {
            var bergoglioService = new BergoglioService();
            var manyCodesResponse = await bergoglioService.GenerateCodes(request.QuantityPerSerie, request.SerialNumberInit, request.SerialNumberFinal);

            return new RandomCodes() { Code = { manyCodesResponse.Codes } };
        }
    }
}
