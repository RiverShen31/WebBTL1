using FluentValidation;
using System.Text.RegularExpressions;
using WebBTL1.Constant;
using WebBTL1.Models;

namespace WebBTL1.Validators
{
	public class EmployeeValidator : AbstractValidator<Employee>
	{
		public EmployeeValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.")
				.Length(Constant.Constant.MinLengthName, Constant.Constant.MaxLengthName).WithMessage("Name must be between 3 and 30 characters.")
				.Must(ValidateName).WithMessage("Invalid characters in the name.");

			RuleFor(x => x.Dob)
				.NotEmpty().WithMessage("Date of Birth is required.")
				.Must(ValidateDob).WithMessage("Invalid Date of Birth");

			RuleFor(x => x.Ethnic)
				.NotEmpty().WithMessage("Ethnic is required.");

			RuleFor(x => x.Job)
				.NotEmpty().WithMessage("Job is required.");

			RuleFor(x => x.IdentityNumber)
				.Length(Constant.Constant.IdentityNumberLength).WithMessage("Identity number must have 11 characters.");

			RuleFor(x => x.PhoneNumber)
				.Length(Constant.Constant.PhoneNumberLength).WithMessage("Phone number must have 10 characters.")
				.Must(IsPhoneNumber).WithMessage("Invalid phone number");

			RuleFor(x => x.Province)
				.NotEmpty().WithMessage("Province is required.");

			RuleFor(x => x.District)
				.NotEmpty().WithMessage("District is required.");

			RuleFor(x => x.Commune)
				.NotEmpty().WithMessage("Commune is required.");
		}

		private bool ValidateName(string name)
		{
			return Regex.IsMatch(name, "^[a-zA-Z ]*$");
		}

		private bool ValidateDob(DateTime dob)
		{
			return Constant.Constant.MinYear <= dob.Year && dob.Year < DateTime.Now.Year;
		}

		private bool IsPhoneNumber(string phoneNumber)
		{
			return string.IsNullOrEmpty(phoneNumber) || phoneNumber.All(char.IsDigit);
		}
	}
}