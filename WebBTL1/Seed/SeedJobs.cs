using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedJobs
    {
        public List<Job> Jobs { get; set; } = new List<Job>
        {
            new Job { JobTitle = "Software Developer" },
            new Job { JobTitle = "Marketing Manager" },
            new Job { JobTitle = "Carpenter" },
            new Job { JobTitle = "Laborer" },
            new Job { JobTitle = "Bricklayer" },
            new Job { JobTitle = "Mason" },
            new Job { JobTitle = "Welder" },
            new Job { JobTitle = "Miner" },
            new Job { JobTitle = "Printer" },
            new Job { JobTitle = "Plater" },
        };

        public SeedJobs()
        {
            // Automatically assign IDs based on the order in the list
            for (var i = 0; i < Jobs.Count; i++)
            {
                Jobs[i].Id = i + 1;
            }
        }
    }
}