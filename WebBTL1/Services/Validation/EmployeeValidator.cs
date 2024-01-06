using FluentValidation;
using WebBTL1.ViewModels;

namespace WebBTL1.Services.Validation
{
	public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
	{
		public EmployeeValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
				.MaximumLength(Constant.Constant.MaxLengthName).WithMessage("Name can not over 100 characters")
				.MinimumLength(Constant.Constant.MinLengthName).WithMessage("Name must greater than 3 characters");

				
		}
	}
}
