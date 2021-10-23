using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthDeclaration.Helpers
{
    public static class CommonHelper
    {
        public static List<string> GetElementByRegex(this string html, string start, string end)
        {
            var regSelect = new Regex($"(?<=({start}))(.*?)(?=({end}))",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var matches = regSelect.Matches(html);
            return matches.Cast<Match>().Select(x => x.Value).ToList(); ;
        }

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
