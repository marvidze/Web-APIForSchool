using System.ComponentModel.DataAnnotations;

namespace School.API.GraphQL.Validation;

public static class InputValidator
{
    public static void Validate<T>(T input)
    {
        var validationResults = new List<ValidationResult>();

        var validationContext = new ValidationContext(input!);

        var isValid = Validator.TryValidateObject(
            input!,
            validationContext,
            validationResults,
            validateAllProperties: true);

        if (isValid)
        {
            return;
        }

        var validationErrors = validationResults
            .Select(error => new
            {
                Fields = error.MemberNames.ToArray(),
                Message = error.ErrorMessage
            })
            .ToArray();

        throw new GraphQLException(
            ErrorBuilder.New()
                .SetMessage("Ошибка валидации входных данных")
                .SetCode("validation.error")
                .SetExtension("validationErrors", validationErrors)
                .Build());
    }
}