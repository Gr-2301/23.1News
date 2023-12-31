using System;
using HighestLowestElectricityPrice.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HighestLowestElectricityPrice
{
    public class HighestLowestPrice
    {
        private readonly ILogger _logger;
        private readonly ISpotService _spotService;

        public HighestLowestPrice(ILoggerFactory loggerFactory ,ISpotService spotService )
        {
            _logger = loggerFactory.CreateLogger<HighestLowestPrice>();
            _spotService = spotService;
        }

        [Function("HighestLowestPrice")]
        public void Run([TimerTrigger("0 13 * * * *",RunOnStartup =true)] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var spotPrices=_spotService.GetSpotMetrics().Result;
            _spotService.SaveSportData(spotPrices);
            if (myTimer.ScheduleStatus is not null ) 
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
           
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
