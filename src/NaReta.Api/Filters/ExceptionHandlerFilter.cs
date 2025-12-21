using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NaReta.Application.DTO;
using NaReta.Common;
using NaReta.Common.Exceptions;

namespace NaReta.Api.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> _logger;

    public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NaRetaExceptionBase)
            HandlerProjectException(context);
        else
        {
            string exception = $"Message: {context.Exception.Message}; StackTrace: {context.Exception.StackTrace}";
            _logger.LogError(exception + context.Exception.ToString());
            ThrowUnknowError(context);
        }
    }

    private void HandlerProjectException(ExceptionContext context)
    {
        var exception = (NaRetaExceptionBase)context.Exception;
        var errorResponse = new ResponseErrorDTO(exception.GetErrors());
        context.HttpContext.Response.StatusCode = exception.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorDTO(ResourceErrorMessages.UNKNOW_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
