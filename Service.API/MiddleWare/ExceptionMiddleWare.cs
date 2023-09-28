using Framework.core.comman;
using Service.API.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Service.API.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly ILoggerService _logger;  // to log error
        private readonly RequestDelegate _next;   // is a function that can process an HTTP request and produce an HTTP response.

        public ExceptionMiddleWare(ILoggerService logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);  
            }
            catch (Exception exception)
            {

                _logger.logError($"{exception.Message}");
             
                await HandleException(context, exception);

            }
        }

        private async Task HandleException(HttpContext context,Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var Response = new CustomResponse()
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Details = "Internal Server Error"
            };

            var Json = JsonSerializer.Serialize(Response);

            await context.Response.WriteAsync(Json);
        }
    }

    
}

/*
     A middleware : is a class that can handle an HTTP request and produce an HTTP response.

     what is next ?
     
     is a function that can process an HTTP request and produce an HTTP response.
 
     what is HttpContext?
    
     
     
 
 */
