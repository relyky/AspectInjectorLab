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
    [CatchAndLog(Title = "亂數產生器")]
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
            _logger.LogWarning("Random => ON:GetRandomGuid");
            return Guid.NewGuid().ToString();        
        }
    }
}
