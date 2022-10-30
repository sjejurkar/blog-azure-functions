using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CloudBuff.Function
{
    public static class QueueBindingFunction
    {
        [FunctionName("QueueBindingFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Queue("notification-queue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> notifications,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation($"HTTP payload={requestBody}");

            notifications.Add(requestBody);

            return new OkObjectResult("Message sent to queue");
        }
    }
}
