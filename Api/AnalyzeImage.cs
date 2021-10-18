using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Api
{
    public static class AnalyzeImage
    {
        [FunctionName("AnalyzeImage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {

            var someObject = new { text = "This is the returnet message" };

            return new OkObjectResult(someObject);
        }
    }
}
