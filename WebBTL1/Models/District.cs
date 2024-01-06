using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBTL1.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Level { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        [ForeignKey("ProvinceId")]
        public Province? Province { get; set; }

        public ICollection<Commune>? Communes { get; set; }
    }
}
