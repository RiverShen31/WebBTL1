using FluentValidation;
using WebBTL1.Models;

namespace WebBTL1.Validators
{
	public class CommuneValidator : AbstractValidator<Commune>
	{
		public CommuneValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(50).WithMessage("Must be between 3 and 30 characters.");

			RuleFor(x => x.Level)
				.NotEmpty().WithMessage("Level is required.");

			RuleFor(x => x.DistrictId)
				.NotEmpty().WithMessage("DistrictId is required.");
		}
	}
}