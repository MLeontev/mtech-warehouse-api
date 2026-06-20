namespace WarehouseApi.Api.Contracts;

public record ValidationProblemResponse(
    string Title, 
    int Status, 
    IDictionary<string, string[]> Errors);