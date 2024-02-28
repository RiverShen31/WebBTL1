using WebBTL1.Models;

namespace WebBTL1.Seed
{
    public class SeedDiploma
    {
        public List<Diploma> DiplomaList { get; set; }
        
        public SeedDiploma()
        {
            DiplomaList = new List<Diploma>
            {
                new Diploma { Name = "Business Management, University Foundation Program" },
                new Diploma { Name = "PG Dip Business and Management (Business Analytics)" },
                new Diploma { Name = "Blockchain" },
                new Diploma { Name = "Digital Content Marketing" },
                new Diploma { Name = "Montessori Classroom Assistant" },
                new Diploma { Name = "Business Administration" },
                new Diploma { Name = "Psychology" },
                new Diploma { Name = "Post Graduate In Management" },
                new Diploma { Name = "Mechanical Engineering" },
                new Diploma { Name = "Library and Information Science Admission, Colleges, Syllabus, Jobs and Scope" },
                new Diploma { Name = "Post Graduate Diploma in Genecology & Obstetrics" },
                new Diploma { Name = "Labor Laws and Labor Welfare" },
                new Diploma { Name = "Hotel Management & Catering Technology" },
                new Diploma { Name = "Journalism and Mass Communication" },
                new Diploma { Name = "Computerized Accounting" },
                new Diploma { Name = "Construction Management" },
                new Diploma { Name = "Hotel Management & Catering Technology" },
                new Diploma { Name = "Marine Engineering" },
                new Diploma { Name = "Medical Lab Technology" },
                new Diploma { Name = "Textile Design" },
                new Diploma { Name = "Electrical and Electronics Engineering" },
                new Diploma { Name = "Electronics & Communication Engineering" },
                new Diploma { Name = "Nutrition & Dietetics" },
                new Diploma { Name = "Biotechnology" },
                new Diploma { Name = "Radiological Therapy" }
            };
            
            // Automatically assign IDs based on the order in the list
            for (var i = 0; i < DiplomaList.Count; i++)
            {
                DiplomaList[i].Id = i + 1;
            }
        }
    }
}
