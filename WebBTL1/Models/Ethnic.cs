using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class Ethnic
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? EthnicName { get; set; }
    }
}
