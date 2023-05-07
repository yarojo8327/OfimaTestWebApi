using Microsoft.AspNetCore.Mvc;

using Ofima.TechnicalTest.Common;
using Ofima.TechnicalTest.Common.Dto;

using System.Net;
using System.Text.Json;

namespace Ofima.TechnicalTest.WebApi.Handlers
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotImplementedException ex)
            {
                await LauchException(HttpStatusCode.InternalServerError,
                  $"Esta funcionalidad aun no ha sido implementada o se encuentra en construcción",
                  ex, context);
            }
            catch (BusinessException ex)
            {
                await LauchException(HttpStatusCode.BadRequest,
                  $"{ex.Message}",
                  ex, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                await LauchException(HttpStatusCode.InternalServerError,
                   $"Ha ocurrido un error inesperado. Intenente nuevamente o comuniquese con el proveedor del servicio",
                   ex, context);
            }

        }

        private static async Task LauchException(HttpStatusCode statusError, string message, Exception exception, HttpContext context)
        {
            BodyResponse<ProblemDetails> response = new()
            {
                Code = (int)statusError,
                IsSuccess = false,
                Message = message,
                Data = new ProblemDetails
                {
                    Status = (int)statusError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = exception.Message,
                }
            };
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            context.Response.StatusCode = (int)statusError;
            string json = JsonSerializer.Serialize(response, serializeOptions);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
