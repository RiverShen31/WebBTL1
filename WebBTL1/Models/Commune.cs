using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBTL1.Models
{
    public class Commune
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
		[MaxLength(Constant.Constant.MaxLengthName, ErrorMessage = "Must from 3 to 30 characters!")]
		public string? Name { get; set; }
        [Required(ErrorMessage = "Level cannot be empty")]
        public string? Level { get; set; }
        [Required(ErrorMessage = "")]
        public int DistrictId { get; set; }
        [Required]
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
    }
}
