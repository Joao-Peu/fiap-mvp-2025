using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FIAPCloudGames.Application.Exceptions;
using FIAPCloudGames.Domain.Exceptions;

namespace FIAPCloudGames.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object response;
            int statusCode;

            // Tratar exceções específicas do domínio
            switch (exception)
            {
                case EntityNotFoundException entityNotFound:
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new
                    {
                        error = "Recurso não encontrado",
                        details = entityNotFound.Message,
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                case DuplicateGameInLibraryException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = "Jogo já existe na biblioteca",
                        details = exception.Message,
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                case DuplicateLibraryException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = "Usuário já possui uma biblioteca",
                        details = exception.Message,
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                case EmailInvalidException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = "Email inválido",
                        details = exception.Message,
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                case ArgumentException argEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = "Dados inválidos",
                        details = argEx.Message,
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    
                    // Em desenvolvimento, mostrar mais detalhes
                    if (_environment.IsDevelopment())
                    {
                        response = new
                        {
                            error = "Ocorreu um erro inesperado",
                            details = exception.Message,
                            stackTrace = exception.StackTrace,
                            innerException = exception.InnerException?.Message,
                            path = context.Request.Path,
                            timestamp = DateTime.UtcNow
                        };
                    }
                    else
                    {
                        response = new
                        {
                            error = "Ocorreu um erro interno do servidor",
                            path = context.Request.Path,
                            timestamp = DateTime.UtcNow
                        };
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return context.Response.WriteAsync(json);
        }
    }
}
