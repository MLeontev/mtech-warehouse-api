using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Api.Contracts;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Api.Extensions;

internal static class ResultExtensions
{
    public static IActionResult ToProblem(this Error error)
    {
        if (error is ValidationError validationError)
        {
            var response = new ValidationProblemResponse(
                Title: "Validation Error",
                Status: StatusCodes.Status400BadRequest,
                Errors: validationError.Errors);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status400BadRequest };
        }

        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var title = error.Type switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Validation => "Validation Error",
            _ => "Internal Server Error"
        };
        
        var problem = new ProblemResponse(title, statusCode, error.Message);

        return new ObjectResult(problem) { StatusCode = statusCode };
    }
}