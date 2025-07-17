namespace NetValidator.Core
{
    public interface IValidator<in T> 
    {
        ValidationResult Validate(T instance);
    }
}
