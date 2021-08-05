using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Net5ConaoleApp.AOP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// 模擬非同步運算
        /// </summary>
        public async Task<int> SumAsync(int a, int b)
        {
            int sum = -1;

            await Task.Run(async delegate
            {
                _logger.LogWarning($"[Sum:START] {DateTime.Now:HH:mm:ss}");
                await Task.Delay(3000);
                sum = a + b;
                _logger.LogWarning($"[Sum:FINISH] {DateTime.Now:HH:mm:ss} => ({a},{b}) => {sum}");

            });

            return sum;
        }

    }
}
