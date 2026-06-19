using FluentValidation.Results;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Application.Common;

public static class ValidationMappingExtensions
{
    public static ValidationError ToValidationError(this IEnumerable<ValidationFailure> failures)
    {
        var errorsDictionary = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());
        
        return new ValidationError(errorsDictionary);
    }
}