using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
    public class FirstMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Delay(1000);
            await context.Response.WriteAsync("MiddleWare_1 ");
            await next(context);     
            stopwatch.Stop();
            Console.WriteLine("Total-Middleware-1:=" + stopwatch.ElapsedMilliseconds); 
        }
    }