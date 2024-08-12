namespace App.Services;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<Error>? Errors { get; set; }
}

public class Error
{
    public string FieldName { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}
