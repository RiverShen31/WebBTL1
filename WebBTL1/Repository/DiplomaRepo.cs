using WebBTL1.Controllers;
using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class DiplomaRepo : IDiplomaRepo
    {
        private readonly ApplicationDbContext _context;

        public DiplomaRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Diploma diploma)
        {
            _context.Diplomas.Add(diploma);
            _context.SaveChanges();
        }

        public void Update(Diploma diploma) 
        {
            _context.Diplomas.Update(diploma);
            _context.SaveChanges();
        }

        public void Remove(Diploma diploma)
        {
            _context.Diplomas.Remove(diploma);
            _context.SaveChanges();
        }

        public Diploma Find(int id)
        {
            return _context.Diplomas.Find(id)!;
        }

        public List<Diploma> GetDiplomaListByPageNumber(int pageNumber)
        {
            return _context.Diplomas.Skip((pageNumber - 1) * Constant.Constant.PageSize)
                                            .Take(Constant.Constant.PageSize).ToList();
        }

        public int GetDiplomaCount()
        {
            return _context.Diplomas.Count();
        }

        public Diploma? GetDiplomaById(int diplomaId)
        {
            return _context.Diplomas.FirstOrDefault(m => m.Id == diplomaId);
        }

        public List<Diploma> GetDiplomaList()
        {
            return _context.Diplomas.ToList();
        }
    }
}
