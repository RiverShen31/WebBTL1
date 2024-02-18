using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedJobs
    {
        public List<Job> Jobs { get; set; } = new() 
        {
            new Job { Id = 1, JobTitle = "Software Developer" },
            new Job { Id = 2, JobTitle = "Marketing Manager" },
            new Job { Id = 3, JobTitle = "Carpenter" },
            new Job { Id = 4, JobTitle = "Laborer" },
            new Job { Id = 5, JobTitle = "Bricklayer" },
            new Job { Id = 6, JobTitle = "Mason" },
            new Job { Id = 7, JobTitle = "Welder" },
            new Job { Id = 8, JobTitle = "Miner" },
            new Job { Id = 9, JobTitle = "Printer" },
            new Job { Id = 10, JobTitle = "Plater" },
        };
    }
}
