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
    [CatchAndLog(Title = "加總器")]
    class SumOpService
    {
        readonly IConfiguration _config;
        readonly ILogger<RandomService> _logger;

        public SumOpService(IConfiguration config, ILogger<RandomService> logger) 
        {
            _config = config;
            _logger = logger;
        }

        public int Sum(int a, int b) 
        {
            int sum = a + b;

            _logger.LogWarning($"[Sum] ({a},{b}) => {sum}");
            return sum;
        }
    }
}
