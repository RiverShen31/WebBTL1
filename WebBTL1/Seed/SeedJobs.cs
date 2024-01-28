using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedJobs
    {
        public List<Job> Jobs { get; set; } = new() 
        {
            new() { Id = 1, JobTitle = "Software Developer" },
            new() { Id = 2, JobTitle = "Marketing Manager" },
            new() { Id = 3, JobTitle = "Carpenter" },
            new() { Id = 4, JobTitle = "Laborer" },
            new() { Id = 5, JobTitle = "Bricklayer" },
            new() { Id = 6, JobTitle = "Mason" },
            new() { Id = 7, JobTitle = "Welder" },
            new() { Id = 8, JobTitle = "Miner" },
            new() { Id = 9, JobTitle = "Printer" },
            new() { Id = 10, JobTitle = "Plater" },
        };
    }
}
