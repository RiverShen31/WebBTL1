using System.Text.RegularExpressions;
using WebBTL1.Models;

namespace WebBTL1.Services.Validation
{
    public abstract class Validate
    { 
        public static string ValidateEmployee(Employee employee)
        {
            if (!ValidateName(employee.Name))
            {
                return "name";
            }
            if (!ValidateDob(employee.Dob))
            {
                return "dob";
            }
            return !IsPhoneNumber(employee.PhoneNumber) ? "phone" : "true";
        }

        private static bool ValidateName(string? name)
        {
            var regexItem = new Regex("^[a-zA-Z ]*$");
            return name != null && regexItem.IsMatch(name);
        }

        private static bool ValidateDob(DateTime dob)
        {
            return Constant.Constant.MinYear <= dob.Year
                   && dob.Year < DateTime.Now.Year;
        }

        private static bool IsPhoneNumber(string? phoneNumber)
        {
            return phoneNumber == null || phoneNumber.All(char.IsDigit);
        }

        public static int ValidatePageNumber(ref int? pageNumber)
        {
            if (pageNumber is null or <= 0) return Constant.Constant.PageDefault;
            return (int)pageNumber;
        }
    }
}
