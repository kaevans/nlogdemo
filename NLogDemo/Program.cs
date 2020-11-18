using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLogDemo
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            bool flag = true;

            do
            {
                var log = LogManager.GetCurrentClassLogger();
                log.Debug("This is a debug message");
                log.Error(new Exception(), "This is an error message");
                log.Fatal("This is a fatal message");

                log.Info("This is a message from {User}", "Mickey Donovan");


                var responseString = string.Empty;
                try
                {
                    responseString = client.GetStringAsync("http://www.msn.com/").GetAwaiter().GetResult();
                    log.Trace(responseString);
                    log.Info("successfully called msn");
                }
                catch(System.Exception oops)
                {
                    log.Error(oops, "Calling msn");
                }
                try
                {
                    responseString = client.GetStringAsync("http://www.microsoft.com/").GetAwaiter().GetResult();
                    log.Trace(responseString);
                    log.Info("successfully called microsoft");
                }
                catch (System.Exception oops)
                {
                    log.Error(oops, "Calling microsoft");
                }
                try
                {
                    responseString = client.GetStringAsync("http://www.bing.com/").GetAwaiter().GetResult();
                    log.Trace(responseString);
                    log.Info("successfully called bing");
                }
                catch (System.Exception oops)
                {
                    log.Error(oops, "calling bing");
                }
                System.Threading.Thread.Sleep(1000);

            } 
            while (flag == true);
        }
    }
}
