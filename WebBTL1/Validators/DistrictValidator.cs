using FluentValidation;
using WebBTL1.Models;

namespace WebBTL1.Validators
{
	public class DistrictValidator : AbstractValidator<District>
	{
		public DistrictValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.");

			RuleFor(x => x.Level)
				.NotEmpty().WithMessage("Level is required.");

			RuleFor(x => x.ProvinceId)
				.NotEmpty().WithMessage("ProvinceId is required.");
		}
	}
}