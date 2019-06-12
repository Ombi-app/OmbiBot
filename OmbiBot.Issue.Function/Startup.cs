using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OmbiBot.Processor;

namespace OmbiBot.Issue.Function
{
    public static class Startup
    {
        public static IServiceProvider CreateContainer()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IApiProcessor, ApiProcessor>();
            services.AddScoped<IProcessor, CreateIssueProcessor>();

            return services.BuildServiceProvider();
        }
    }
}
