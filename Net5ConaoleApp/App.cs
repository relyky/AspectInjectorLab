using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net5ConaoleApp.Services;

namespace Net5ConaoleApp
{
    class App
    {
        readonly IConfiguration _config;
        readonly RandomService _randSvc;
        readonly SumOpService _sumSvc;

        public App(IConfiguration config, RandomService randSvc, SumOpService sumSvc)
        {
            _config = config;
            _randSvc = randSvc;
            _sumSvc = sumSvc;
        }

        /// <summary>
        /// 取代原本 Program.Main() 函式的效用。
        /// </summary>
        public void Run(string[] args)
        {
            Console.WriteLine($"App => {_config.GetConnectionString("DefaultConnection")}");
            Console.WriteLine($"App => {_sumSvc.Sum(234, 432)}");
            Console.WriteLine($"App => {_randSvc.GetRandomGuid()}");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
