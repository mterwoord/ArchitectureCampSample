using System.Reflection;
using Hosting;
using Microsoft.Owin.Hosting;
using System;

namespace SelfHost
{
    class Program
    {
        static void Main()
        {
            var a = Assembly.Load("ServicesLayer");

            using (WebApp.Start<Startup>("http://localhost:7777/"))
            {
                Console.WriteLine("Architecture Camp - EndToEnd Server running...");
                Console.ReadLine();                
            }
        }
    }
}
