using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBTL1.ViewModels
{
    public class AwardDiplomaViewModel
    {
        public AwardDiplomaViewModel() { }

        public AwardDiplomaViewModel(int id, int employeeId, string? employee,
                                        int diplomaId, string? diploma, int diplomaGrantingUnitId,
                                        string? diplomaGrantingUnit, DateTime grantingDate, int duration) 
        {
            Id = id;
            EmployeeId = employeeId;
            Employee = employee;
            DiplomaId = diplomaId;
            Diploma = diploma;
            DiplomaGrantingUnitId = diplomaGrantingUnitId;
            DiplomaGrantingUnit = diplomaGrantingUnit;
            GrantingDate = grantingDate;
            Duration = duration;
        }
        public int Id { get; set; }

        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        public string? Employee { get; set; }

        [Required]
        [DisplayName("Diploma")]
        public int DiplomaId { get; set; }

        public string? Diploma { get; set; }

        [Required]
        [DisplayName("Diploma Granting Unit")]
        public int DiplomaGrantingUnitId { get; set; }

        public string? DiplomaGrantingUnit { get; set; }
        [Required]
        [DisplayName("Granting Date")]
        public DateTime GrantingDate { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
