using FluentValidation.Results;

namespace App.Services.Extensions;

public static class ModelStateExtensions
{
    public static void AddToErrorsModel(this ValidationResult result, List<Error> errors)
    {
        foreach (var error in result.Errors)
        {
            errors.Add(new Error() { FieldName = error.PropertyName, ErrorMessage = error.ErrorMessage });
        }
    }
}
