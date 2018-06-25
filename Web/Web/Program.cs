using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //                    WebHost.CreateDefaultBuilder(args)
        //                    .UseUrls("http://localhost:7002;http://*:7003")		//指定端口
        //                    .UseKestrel()   //应用为服务器使用的web主机。
        //                    .UseContentRoot(Directory.GetCurrentDirectory())
        //                    .UseIISIntegration()
        //                    .UseStartup<Startup>()
        //                    .Build();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:7002;http://*:7003")		//指定端口
                .UseKestrel()   //应用为服务器使用的web主机。
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
