using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();

            System.Diagnostics.Debug.WriteLine("hello debugger!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        [HttpPost]
        [ActionName("Simple")]
        public HttpResponseMessage PostSimple([FromBody] string value)
        {
            if (value != null)
            {

                var response = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
                return response;
            }
            else
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);

                return response;
            }


        }

    }
}
