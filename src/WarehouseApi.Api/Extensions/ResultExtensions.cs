using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Api.Contracts;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Api.Extensions;

internal static class ResultExtensions
{
    public static IActionResult ToProblem(this Error error, ControllerBase controller)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var title = error.Type switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Internal Server Error"
        };
        
        return controller.Problem(
            detail: error.Message, 
            statusCode: statusCode, 
            title: title);
    }
}