using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace func
{
    public static class GetSettingInfo
    {
     [FunctionName("GetSettingInfo")]
     //Specify that the name of the Azure Function is "GetSettingInfo".
     public static IActionResult Run(        
         [HttpTrigger(AuthorizationLevel.Anonymous, "GET")] HttpRequest request,
         [Blob("content/currency.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string json)
         => new OkObjectResult(json);  
    }
}
