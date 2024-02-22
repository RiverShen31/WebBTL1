using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class AwardDiploma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        [DisplayName("Diploma")]
        public int DiplomaId { get; set; }

        [Required]
        [DisplayName("Diploma Granting Unit")]
        public int DiplomaGrantingUnitId { get; set; }

        [Required]
        [DisplayName("Granting Date")]
        public DateTime GrantingDate { get; set; }

        [Required]
        public int Duration { get; set; }
 
    }
}
