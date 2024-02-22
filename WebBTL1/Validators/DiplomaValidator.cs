using FluentValidation;
using WebBTL1.Models;

namespace WebBTL1.Validators
{
    public class DiplomaValidator :AbstractValidator<Diploma>
    {
        public DiplomaValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
