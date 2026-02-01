using Azure;
using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commers.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _Logger;
        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _Logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandlNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Something Went Wrong");
                await HandleExceptionAsync(context, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            //set Status code for Respons
            var Resp = new ErrorToReturn
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException =>StatusCodes.Status401Unauthorized,
                BadRequestException badRequest => GetBadRequestErrors(badRequest,Resp),
                _ => StatusCodes.Status500InternalServerError
            };
            // context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

            //set content type Respons
           // context.Response.ContentType = "aplication/json";

            //Respons Object 
        
            await context.Response.WriteAsJsonAsync(Resp);
        }

        private static int GetBadRequestErrors(BadRequestException badRequest, ErrorToReturn resp)
        {
            resp.Errors = badRequest.error;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandlNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Res = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The EndPoint {context.Request.Path} Is NoT Found..."
                };

                await context.Response.WriteAsJsonAsync(Res);
            }
        }
    }
}
