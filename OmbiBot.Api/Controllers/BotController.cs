using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmbiBot.Processor;

namespace OmbiBot.Api.Controllers
{
    [Route("api/[controller]")]
    public class BotController : Controller
    {
        public BotController(IProcessor p)
        {
            Processor = p;
        }

        private IProcessor Processor { get; }
        // POST api/Bot
        [HttpPost]
        public async Task Post([FromBody]GithubIssuePayload payload)
        {
            if (string.IsNullOrEmpty(payload.action))
            {
                return;
            }
            if (payload.action.Equals("Created", StringComparison.CurrentCultureIgnoreCase) || payload.action.Equals("Opened", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Issue Created");
                await Processor.Process(payload);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
