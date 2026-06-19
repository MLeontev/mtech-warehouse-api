namespace WarehouseApi.Domain.Common;

public record Error(string Code, string Message, ErrorType Type);