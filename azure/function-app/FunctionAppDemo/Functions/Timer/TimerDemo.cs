using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.Functions.Timer
{
    public static class TimerDemo
    {
        [FunctionName("TimerDemo")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            if (myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }

            log.LogInformation($"Hello! It's {DateTime.Now}.");
        }
    }
}
