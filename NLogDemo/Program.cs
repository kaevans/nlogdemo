using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NLogDemo
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {            
            client.Timeout = new TimeSpan(0, 0, 3);
            do
            {
                var log = LogManager.GetCurrentClassLogger();
                log.Debug("This is a debug message");
                                
                log.Info("This is a message from {User}", WindowsIdentity.GetCurrent().Name);

                GetHttpStringAsync("http://www.msn.com").GetAwaiter().GetResult();
                GetHttpStringAsync("http://www.microsoft.com").GetAwaiter().GetResult();
                GetHttpStringAsync("http://www.bing.com").GetAwaiter().GetResult();
                GetHttpStringAsync("https://doesntexist/").GetAwaiter().GetResult();

                System.Threading.Thread.Sleep(1000);

            } 
            while (true);
        }

        private static async Task<string> GetHttpStringAsync(string url)
        {
            //Set timeout for 3 seconds
            
            var log = LogManager.GetCurrentClassLogger();
            
            var responseString = default(string);

            try
            {
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                log.Info("{Status}: {Url}", response.StatusCode, url);
                
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                log.Trace(content);

            }
            catch (System.Exception oops)
            {
                log.Error(oops, "Error calling {Url}", url);
            }

            return responseString;
        }
    }
}
