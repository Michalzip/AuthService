using Newtonsoft.Json;

namespace AuthService.Shared.Middlewares
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ExceptionBase ex)
            {
                var exceptionResponse = new ExceptionResponse(ex);

                context.Response.ContentType = "application/json";

                var errorMessage = JsonConvert.SerializeObject(exceptionResponse);

                await context.Response.WriteAsync(errorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}