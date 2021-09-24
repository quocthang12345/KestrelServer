using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace mywebapp.middleware
{
    public class ThirdMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Delay(1000);
            await context.Response.WriteAsync("MiddleWare_3 ");
            await next(context);     
            stopwatch.Stop();
            Console.WriteLine("Total-Middleware-3:=" + stopwatch.ElapsedMilliseconds); 
        }
    }
}