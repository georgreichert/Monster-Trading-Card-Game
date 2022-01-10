using Server.Core.Request;
using Server.Core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{
    class MTCGRouteParser : IRouteParser
    {
        public bool IsMatch(RequestContext request, HttpMethod method, string routePattern)
        {
            if (method != request.Method)
            {
                return false;
            }

            string[] patterns = { 
                "^" + routePattern.Replace("{username}", ".*").Replace("/", "\\/") + "$",
                "^" + (routePattern + "\\?[_A-Za-z0-9]+=.*").Replace("/", "\\/") + "$"
            };
            foreach (string pattern in patterns)
            {
                if (Regex.IsMatch(request.ResourcePath, pattern))
                {
                    return true;
                }
            }
            return false;
        }

        public Dictionary<string, Dictionary<string, string>> ParseParameters(RequestContext request, string routePattern)
        {
            var parameters = new Dictionary<string, Dictionary<string, string>>();
            Tuple<string, string>[] patterns = {
                new Tuple<string, string>("username", "^" + routePattern.Replace("{username}", "(?<username>[^?]*)?.*").Replace("/", "\\/") + "$")
            };

            parameters["params"] = new Dictionary<string, string>();

            foreach (Tuple<string, string> pattern in patterns)
            {
                var result = Regex.Match(request.ResourcePath, pattern.Item2);
                if (result.Groups[pattern.Item1].Success)
                {
                    parameters["params"][pattern.Item1] = result.Groups[pattern.Item1].Captures[0].Value;
                }
            }

            parameters["urlParams"] = new Dictionary<string, string>();
            string getUrlPattern = "\\?[_A-Za-z0-9]+=[^&=]*(&[_A-Za-z0-9]+=[^&=]*)*";

            if (Regex.IsMatch(request.ResourcePath, getUrlPattern))
            {
                string getUrlParamPattern = "[_A-Za-z0-9]+=[^&=]*";
                var getUrlResult = Regex.Matches(request.ResourcePath, getUrlParamPattern);
                foreach (Match match in getUrlResult)
                {
                    string[] keyValuePair = match.Value.Split("=");
                    parameters["urlParams"][keyValuePair[0]] = keyValuePair[1];
                }
            }

            return parameters;
        }
    }
}
