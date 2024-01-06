using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Level { get; set; }

        public ICollection<District>? Districts { get; set; }
    }
}
