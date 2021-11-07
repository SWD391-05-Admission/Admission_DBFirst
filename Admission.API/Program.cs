using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Admission.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //Thread t2 = new Thread(() =>
            //{
            //    StartThread2(args);
            //});
            //t2.Start();

            //Thread t1 = new Thread(() =>
            //{
            //    StartThread1();
            //});
            //t1.Start();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void StartThread1()
        {
            TalkshowManagementService _iTalkshowManagementService = new();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            _iTalkshowManagementService.FinishTalkshow();
        }

        static void StartThread2(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
