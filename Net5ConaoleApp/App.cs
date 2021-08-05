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

        public App(IConfiguration config, RandomService randSvc)
        {
            _config = config;
            _randSvc = randSvc;
        }

        /// <summary>
        /// 取代原本 Program.Main() 函式的效用。
        /// </summary>
        public void Run(string[] args)
        {
            Console.WriteLine("Yes, 成功執行了。");

            // 測試 services injection
            Console.WriteLine($"{_config.GetConnectionString("DefaultConnection")}");
            Console.WriteLine($"{_randSvc.GetRandomGuid()}");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
