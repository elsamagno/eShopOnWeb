using Microsoft.AspNetCore.Builder;
using Web.Middleware;
using Microsoft.eShopWeb.Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class BenchmarkingMiddlewareExtensions
    {
        public static void UseBenchmarking(this IApplicationBuilder app) {
            app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            
        }
    }
}