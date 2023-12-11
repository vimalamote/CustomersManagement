using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CustomerManagementApi.Middleware
{
    public static class GlobalExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(
        this IApplicationBuilder app)

        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (contextFeature != null)
                    {
                        var errorMessage = contextFeature.Error.Message;
                        await context.Response.WriteAsync(errorMessage);
                    }
                });
            });
        }
    }
}
