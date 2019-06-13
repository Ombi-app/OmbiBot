using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OmbiBot.Processor;

namespace OmbiBot.Issue.Function
{
    public static class GithubIssueCheck
    {
        [FunctionName("GithubIssueCheck")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var services = Startup.CreateContainer(log);

            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<GithubIssuePayload>(requestBody);

            if (data.action.Equals("Opened", StringComparison.InvariantCultureIgnoreCase))
            {
                var processor = services.GetRequiredService<IProcessor>();
                await processor.Process(data);
            }

            log.LogInformation("Finished processing issue");
            return new OkResult();
        }
    }
}
