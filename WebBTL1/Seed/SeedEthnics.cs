using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedEthnics
    {
        public List<Ethnic> EthnicGroups { get; set; }

        public SeedEthnics()
        {
            EthnicGroups = new List<Ethnic>
            {
                new Ethnic { EthnicName = "Kinh" },
                new Ethnic { EthnicName = "Tay" },
                new Ethnic { EthnicName = "Muong" },
                new Ethnic { EthnicName = "Thai" },
                new Ethnic { EthnicName = "Khmer" },
                new Ethnic { EthnicName = "Hoa" },
                new Ethnic { EthnicName = "Dao" },
                new Ethnic { EthnicName = "Hmong" },
                new Ethnic { EthnicName = "Nung" },
                new Ethnic { EthnicName = "Cham" }
            };

            // Automatically assign IDs based on the order in the list
            for (var i = 0; i < EthnicGroups.Count; i++)
            {
                EthnicGroups[i].Id = i + 1;
            }
        }
    }
}