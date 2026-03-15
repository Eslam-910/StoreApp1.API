using Domain.Exceptions;
using Shared.ErrorModels;

namespace StoreApp.API.Middlewares
{
    public class GlobalHandlingErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalHandlingErrorMiddleware> _logger;

        public GlobalHandlingErrorMiddleware(RequestDelegate next,ILogger<GlobalHandlingErrorMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandlingNotFoundEndPointAsync(context);

                }
            }
            catch (Exception ex)
            {
                //log
                _logger.LogError(ex, ex.Message);

                await HandlingErrorAsync(context, ex);

            }

        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            //1.set statuscode for response
            //2.set content type code for response
            //3.response object (body)
            //4.return Response

            //context.Response.StatusCode=StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {

                ErrorMessage = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {context.Request.Path} Is not found"
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
