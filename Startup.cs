using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mywebapp.middleware;

namespace mywebapp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<FirstMiddleware>();
            services.AddSingleton<SecondMiddleware>();
            services.AddSingleton<ThirdMiddleware>();
            services.AddSingleton<DiagnosticObserver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DiagnosticListener diagnosticListenerSource,DiagnosticObserver diagnosticObserver)
        {
            
            diagnosticListenerSource.Subscribe(diagnosticObserver);

            app.UseRouting();

            app.UseMiddleware<FirstMiddleware>();

            app.UseMiddleware<SecondMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/example", async context => {
                    await context.Response.WriteAsync("Example Page");
                });
            });

            app.UseMiddleware<ThirdMiddleware>();

            app.Run(async (context) => {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                // await Task.Delay(1000);
                await context.Response.WriteAsync("MiddleWare_4 ");   
                stopwatch.Stop();
                Console.WriteLine("Total-Middleware-4:=" + stopwatch.ElapsedMilliseconds); 
                // var stopwatch = Stopwatch.StartNew();
                // Thread.Sleep(1000);
                // await context.Response.WriteAsync($"Finished in {stopwatch.ElapsedMilliseconds} milliseconds.");
            });
        }
    }
}
