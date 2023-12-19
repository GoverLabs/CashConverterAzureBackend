using System;
using System.IO;
using System.Net;
using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace func
{
    public class Recurring
    {
        [FunctionName("Recurring")]
        public void Run([TimerTrigger("0 0 6 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string url = "https://api.privatbank.ua/p24api/exchange_rates?json&date={0}";
            string date = DateTime.Now.ToString("dd.MM.yyyy");

            string query = String.Format(url, date);
            log.LogInformation(query);

            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(query);

            CreateBlob(result);
        }

        private void CreateBlob(string content)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=funcstorniksmirnovlab2;AccountKey=YUvvvJiYDlL5uP/o8IrKINoACLzWV47IFbCygxLKBC65GVMLNDe9uoz4HO8vuylN4Pt71gQ0fNZY+AStF7b0SA==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("content");
            BlobClient blobClient = containerClient.GetBlobClient("currency.json");

            var contentBytes = Encoding.UTF8.GetBytes(content);

            bool overwrite = true;
            using(var ms = new MemoryStream(contentBytes))
                blobClient.Upload(ms, overwrite);
        }
    }
}
