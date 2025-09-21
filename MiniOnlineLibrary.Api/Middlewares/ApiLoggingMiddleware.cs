using AutoMapper;
using MiniOnlineLibrar.Infrastructure;
using MiniOnlineLibrary.Application.DTO;
using MiniOnlineLibrary.Application.Interfaces;
using System.Diagnostics;

namespace MiniOnlineLibrary.Api.Middlewares
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Resolve scoped service correctly within the request scope
            var logService = context.RequestServices.GetRequiredService<IAPILogService>();

            context.Request.EnableBuffering();

            string requestBody = "";
            if (context.Request.ContentType?.Contains("application/json") == true)
            {
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);

                stopwatch.Stop();

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                string ipAddress = context.Connection.RemoteIpAddress?.ToString();
                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                }
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "id");
                int userId = 0;
               
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out  userId))
                {

                    // ✅ You now have the userId
                }
                int? userIdNullable = userId == 0 ? null : userId;
                var apiLog = new ApiLogsDTO
                {
                    endpoint = context.Request.Path,
                    method = context.Request.Method,
                    request_body = requestBody,
                    response_body = responseText,
                    status_code = context.Response.StatusCode,
                    response_time_ms = stopwatch.ElapsedMilliseconds,
                    ip_address = ipAddress,
                    created_at = DateTime.UtcNow,
                    user_id = userIdNullable

                };

                await logService.CreateAsync(apiLog);

                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }

}
