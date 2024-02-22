using System.ComponentModel.DataAnnotations;

namespace WebBTL1.Models
{
    public class Diploma
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
