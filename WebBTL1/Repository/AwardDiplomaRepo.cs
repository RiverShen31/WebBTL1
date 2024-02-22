using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class AwardDiplomaRepo : IAwardDiplomaRepo
    {
        private readonly ApplicationDbContext _context;
        public AwardDiplomaRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(AwardDiploma awardDiploma)
        {
            _context.AwardDiplomas.Add(awardDiploma);
            _context.SaveChanges();
        }

        public void Update(AwardDiploma awardDiploma)
        {
            _context.AwardDiplomas.Update(awardDiploma);
            _context.SaveChanges();
        }

        public void Remove(AwardDiploma awardDiploma)
        {
            _context.AwardDiplomas.Remove(awardDiploma);
            _context.SaveChanges();
        }

        public AwardDiploma Find(int id)
        {
            return _context.AwardDiplomas.Find(id)!;
        }

        public List<AwardDiploma?> GetAwardDiplomaListByPageNumber(int pageNumber)
        {
            return _context.AwardDiplomas.Skip((pageNumber - 1) * Constant.Constant.PageSize)
                                            .Take(Constant.Constant.PageSize).ToList();
        }

        public int GetAwardDiplomaCount()
        {
            return _context.AwardDiplomas.Count();
        }

        public AwardDiploma? GetAwardDiplomaById(int diplomaId)
        {
            return _context.AwardDiplomas.FirstOrDefault(m => m.Id == diplomaId);
        }

        public int CountNumberOfDiplomaOfEachEmployee(int employeeId, int currentRecordId)
        {
            return _context.AwardDiplomas.Count(ad => ad.EmployeeId == employeeId && ad.Id != currentRecordId);
        }

        public bool HasDuplicateDiplomaIdForEmployee(int employeeId, int diplomaId, int currentRecordId)
        {
            // Check if there is any other record with the same DiplomaId for the same EmployeeId
            return _context.AwardDiplomas.Any(ad => ad.EmployeeId == employeeId && ad.DiplomaId == diplomaId
                                                && ad.Id != currentRecordId);
        }
    }
}
