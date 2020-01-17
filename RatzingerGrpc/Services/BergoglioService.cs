using Bergogliov1;
using Grpc.Net.Client;
using System.Threading.Tasks;


namespace RatzingerGrpc.Services
{
    public class BergoglioService
    {
        public async Task<ManyCodesResponse> GenerateCodes(int quantityPerSerie, int serialNumberInit, int serialNumberFinal)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:58147");
            var client = new Bergoglio.BergoglioClient(channel);
            var manyCodesResponse = await client.GetManyCodesAsync(
               new ManyCodesRequest()
               {
                   QuantityPerSerie = quantityPerSerie,
                   SerialNumberInit = serialNumberInit,
                   SerialNumberFinal = serialNumberFinal
               });
            return manyCodesResponse;
        }
    }
}
