using FluentValidation;

namespace CartonCaps.Shared.Extensions
{
    public static class IValidatorExtensions
    {
        public static AbstractValidator<T>? GetValidator<T>(this IEnumerable<IValidator> validators)
        {
            return validators.OfType<AbstractValidator<T>>().FirstOrDefault();
        }
    }
}
