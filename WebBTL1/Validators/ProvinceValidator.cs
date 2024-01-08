using FluentValidation;
using WebBTL1.Models;

namespace WebBTL1.Validators
{
	public class ProvinceValidator : AbstractValidator<Province>
	{
		public ProvinceValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.");

			RuleFor(x => x.Level)
				.NotEmpty().WithMessage("Level is required.");
		}
	}
}