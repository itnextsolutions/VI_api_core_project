using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace VastraindiaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder
                
                 //.UseContentRoot(Directory.GetCurrentDirectory())
                 .UseKestrel()
                 .UseIISIntegration()
                 .UseIIS()
                 .UseStartup<Startup>();
                 //.Build();
             });


    }
}
