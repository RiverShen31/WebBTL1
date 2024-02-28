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

		public string? Name { get; set; }

		public DateTime Dob { get; set; }

		public int Age { get; set; }

		public string? Ethnic { get; set; }

		public string? Job { get; set; }

		public string? IdentityNumber { get; set; }

		public string? PhoneNumber { get; set; }

		public int Province { get; set; }

		public int District { get; set; }

		public int Commune { get; set; }
		
		public string? Description { get; set; }
		
		public static int SetAge(DateTime dob)
		{
			return DateTime.Now.Year - dob.Year;
		}

	}
}
