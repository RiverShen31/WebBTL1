using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class Job
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? JobTitle { get; set; }
    }
}
