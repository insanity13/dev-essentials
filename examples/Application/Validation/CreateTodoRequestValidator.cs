using Application.DTOs;
using NetValidator.Core;

namespace Application.Validation
{
    internal class CreateTodoRequestValidator : IValidator<CreateTodoRequest>
    {
        public ValidationResult Validate(CreateTodoRequest? request) 
        {
            var errors = new Dictionary<string, string[]>(); // TODO: do not allocate

            if (string.IsNullOrWhiteSpace(request?.Title))
                errors.Add(nameof(request.Title), ["Title is required"]);
            else if (request.Title.Length > 100)
                errors.Add(nameof(request.Title), ["Title must be less than 100 characters"]);

            if (request?.Description?.Length > 500)
                errors.Add(nameof(request.Description), ["Description must be less than 500 characters"]);


            return new ValidationResult(isValid: errors.Count == 0, errors);
        }

        public ValidationResult Validate(object instance) => Validate(instance as CreateTodoRequest);
    }
}
