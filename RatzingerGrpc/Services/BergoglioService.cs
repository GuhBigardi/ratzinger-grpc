using Bergogliov1;
using Grpc.Core;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace RatzingerGrpc.Services
{
    public class BergoglioService
    {
        //public async Task<ManyCodesResponse> GenerateCodes(int quantityPerSerie, int serialNumberInit, int serialNumberFinal)
        //{
        //    var channel = new Channel("localhost:58147", ChannelCredentials.Insecure);
        //    var client = new Bergoglio.BergoglioClient(channel);
        //    var manyCodesResponse = await client.GetManyCodesAsync(
        //       new ManyCodesRequest()
        //       {
        //           QuantityPerSerie = quantityPerSerie,
        //           SerialNumberInit = serialNumberInit,
        //           SerialNumberFinal = serialNumberFinal
        //       });
        //    return manyCodesResponse;
        //}

        public async Task GenerateCodes(int quantityPerSerie, int serialNumberInit, int serialNumberFinal)
        {
            StreamWriter streamWriter;
            string filePath = ".\\codes.txt";
            streamWriter = File.CreateText(filePath);

            var channel = new Channel("localhost:8563", ChannelCredentials.Insecure);
            var client = new Bergoglio.BergoglioClient(channel);
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(15));
            using var streamingCall = client.GetManyCodesStream(new ManyCodesRequest()
            {
                QuantityPerSerie = quantityPerSerie,
                SerialNumberInit = serialNumberInit,
                SerialNumberFinal = serialNumberFinal
            }, cancellationToken: cts.Token);



            try
            {
                await foreach (var codesResponse in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    streamWriter.Write(codesResponse.Codes);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
            finally
            {
                streamWriter.Close();
            }


        }
    }
}
