using Newtonsoft.Json.Schema;

namespace OmbiBot.Processor
{
    public class AdditionalActions
    {
        public string Process(string body)
        {
            if (body.Contains("System.Net.HttpListenerException: Access is denied"))
            {
                return AccessDenied();
            }

            return string.Empty;
        }

        private string AccessDenied()
        {

            return @"Hi! 
                    It looks like you need to run as administrator since you have the following error `System.Net.HttpListenerException: Access is denied`.
                    Can you please run Ombi as administrator and try again, if you do not want to run as administrator take a look at this in the FAQ: https://github.com/tidusjar/Ombi/wiki/FAQ#how-can-i-run-plex-requestsnet-as-a-non-administrator";
        }
    }
}