namespace WarehouseApi.Domain.Common;

public record ValidationError(IDictionary<string, string[]> Errors) 
    : Error("Validation.Error", "Произошла одна или несколько ошибок валидации.", ErrorType.Validation);