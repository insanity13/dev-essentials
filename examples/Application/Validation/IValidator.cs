using NetResults.Core;
using System.ComponentModel.DataAnnotations;

namespace Application.Validation
{
    public interface IValidator<T>
    {
        NetResults.Core.ValidationResult Validate(T request);
    }
}
