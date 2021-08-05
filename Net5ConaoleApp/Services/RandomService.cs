using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Net5ConaoleApp.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5ConaoleApp.Services
{
    [CatchAndLog]
    class RandomService
    {
        readonly IConfiguration _config;
        readonly ILogger<RandomService> _logger;

        public RandomService(IConfiguration config, ILogger<RandomService> logger) 
        {
            _config = config;
            _logger = logger;
        }

        public string GetRandomGuid() 
        {
            _logger.LogWarning("Random => ON:GetRandomGuid...");

            // 測試 services injection
            Console.WriteLine($"Random => {_config["OutputFolder"]}");

            return Guid.NewGuid().ToString();        
        }
    }
}
