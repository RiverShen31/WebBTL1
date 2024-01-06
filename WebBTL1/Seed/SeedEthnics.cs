using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedEthnics
    {
        public List<Ethnic> EthnicGroups { get; set; }
        public SeedEthnics() 
        { 
            EthnicGroups = new List<Ethnic>() 
            {
                new() { Id = 1, EthnicName = "Kinh" },
                new() { Id = 2, EthnicName = "Tay" },
                new() { Id = 3, EthnicName = "Muong" },
                new() { Id = 4, EthnicName = "Thai"},
                new() { Id = 5, EthnicName = "Khmer"},
                new() { Id = 6, EthnicName = "Hoa"},
                new() { Id = 7, EthnicName = "Dao"},
                new() { Id = 8, EthnicName = "Hmong"},
                new() { Id = 9, EthnicName = "Nung"},
                new() {Id = 10, EthnicName = "Cham"}
            };
        }
    }
}
