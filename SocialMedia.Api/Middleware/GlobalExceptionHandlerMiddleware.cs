using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SigefMunicipal.Domain.Responses;
using SocialMedia.Core.AppExceptions;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMedia.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Response.ContentType = "application/json";

                string json = JsonSerializer.Serialize(ApiResponse<string>.Fail(e.Message));

                if(e is AppException)
                {
                    json = JsonSerializer.Serialize(
                        ApiResponse<AppException>.Fail((e as AppException).Errors.ToArray(), e.Message)
                        );
                    context.Response.StatusCode = (e as AppException).Code;
                }

                await context.Response.WriteAsync(json);

            }
        }
    }
}
