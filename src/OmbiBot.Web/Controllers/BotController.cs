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
        public void Post([FromBody]GithubIssuePayload payload)
        {
            if (payload.action.Equals("Created", StringComparison.CurrentCultureIgnoreCase))
            {
                IProcessor p = new CreateIssueProcessor();
                p.Process(payload);
            }
    

        }
    }
}
