using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OmbiBot.Processor;

namespace OmbiBot.Web.Controllers
{
    [Route("api/[controller]")]
    public class BotController : Controller
    {
        // POST api/Bot
        [HttpPost]
        public async Task Post([FromBody]GithubIssuePayload payload)
        {
            if (payload.action.Equals("Created", StringComparison.CurrentCultureIgnoreCase))
            {
                var p = new CreateIssueProcessor();
                await p.Process(payload);
            }
    

        }
    }
}
