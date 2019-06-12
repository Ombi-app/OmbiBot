using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<GithubIssuePayload>(requestBody);
            var action = data?.action;

            return action != null
                ? (ActionResult)new OkObjectResult($"Hello, {action}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
