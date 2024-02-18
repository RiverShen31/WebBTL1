using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedEthnics
    {
        public List<Ethnic> EthnicGroups { get; set; } = new() 
        {
            new Ethnic { Id = 1, EthnicName = "Kinh" },
            new Ethnic { Id = 2, EthnicName = "Tay" },
            new Ethnic { Id = 3, EthnicName = "Muong" },
            new Ethnic { Id = 4, EthnicName = "Thai"},
            new Ethnic { Id = 5, EthnicName = "Khmer"},
            new Ethnic { Id = 6, EthnicName = "Hoa"},
            new Ethnic { Id = 7, EthnicName = "Dao"},
            new Ethnic { Id = 8, EthnicName = "Hmong"},
            new Ethnic { Id = 9, EthnicName = "Nung"},
            new Ethnic {Id = 10, EthnicName = "Cham"}
        };
    }
}
