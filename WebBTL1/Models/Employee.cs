using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class Employee
	{
        public Employee() { }
        public Employee(int id, string? name, DateTime dob, int age, string? ethnic,
                        string? job, string? identityNumber, string? phoneNumber,
                        int provinceId, int districtId, int communeId, string? description)
        {
            Id = id;
            Name = name;
            Dob = dob;
            Age = age;
            Ethnic = ethnic;
            Job = job;
            IdentityNumber = identityNumber;
            PhoneNumber = phoneNumber;
            Province = provinceId;
            District = districtId;
            Commune = communeId;
            Description = description;
        }

        [Key]
		public int Id { get; set; }

		[Display(Name = "Name")]
		[Required]
		[MaxLength(Constant.Constant.MaxLengthName, ErrorMessage = "Must from 3 to 30 characters!")]
		public string? Name { get; set; }

		[Display(Name = "Date of Birth")]
		[Required(ErrorMessage = "Enter the issued date.")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime Dob { get; set; }

		[Display(Name = "Age")]
		public int Age { get; set; }

		[Display(Name = "Ethnic")]
		[Required(ErrorMessage = "Ethnic cannot be empty!")]
		public string? Ethnic { get; set; }

		[Display(Name = "Job")]
		[Required(ErrorMessage = "Job cannot be empty!")]
		public string? Job { get; set; }

		[Display(Name = "IdentityNumber")]
		[StringLength(Constant.Constant.IdentityNumberLength,
			MinimumLength = Constant.Constant.IdentityNumberLength,
			ErrorMessage = "Identity number must have 11 character!")]
		public string? IdentityNumber { get; set; }

		[Display(Name = "PhoneNumber")]
		[StringLength(Constant.Constant.PhoneNumberLength,
			MinimumLength = Constant.Constant.PhoneNumberLength,
			ErrorMessage = "Phone number must have 10 character!")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Province cannot be empty")]
		public int Province { get; set; }

		[Required(ErrorMessage = "District cannot be empty")]
		public int District { get; set; }

		[Required(ErrorMessage = "Commune cannot be empty")]
		public int Commune { get; set; }
		public string? Description { get; set; }

		public static int SetAge(DateTime dob)
		{
			return DateTime.Now.Year - dob.Year;
		}

	}
}
