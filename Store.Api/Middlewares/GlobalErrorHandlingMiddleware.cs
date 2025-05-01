using Shared;

namespace Store.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message);

                // 1. Set Status Code For Response
                // 2. Set Content Type
                // 3. Response Body
                // 4. Return the response

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
