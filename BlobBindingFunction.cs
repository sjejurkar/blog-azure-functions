using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CloudBuff.Function
{
    public static class BlobBindingFunction
    {
        [FunctionName("BlobBindingFunction")]
        [return: Blob("binding-container/{sys.randguid}.txt")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation($"HTTP payload={requestBody}");

            return requestBody;
        }
    }
}
