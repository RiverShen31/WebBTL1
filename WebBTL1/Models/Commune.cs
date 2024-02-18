using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBTL1.Models
{
    public class Commune
    {
        [Key]
        public int Id { get; set; }
		public string? Name { get; set; }
        public string? Level { get; set; }
        public int DistrictId { get; set; }
        [Required]
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
    }
}
