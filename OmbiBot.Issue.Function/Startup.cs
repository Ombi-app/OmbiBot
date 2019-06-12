using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmbiBot.Processor;

namespace OmbiBot.Issue.Function
{
    public static class Startup
    {
        public static IServiceProvider CreateContainer(ILogger log)
        {
            var services = new ServiceCollection();

            services.AddSingleton<ILogger>(x => log);
            services.AddSingleton<IApiProcessor, ApiProcessor>();
            services.AddScoped<IProcessor, CreateIssueProcessor>();

            return services.BuildServiceProvider();
        }
    }
}
