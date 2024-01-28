using Microsoft.EntityFrameworkCore;
using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class CommuneRepo : ICommuneRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistrictRepo _districtRepo;
        public CommuneRepo(ApplicationDbContext context,
            IDistrictRepo districtRepo)
        {
            _context = context;
            _districtRepo = districtRepo;
        }

        public void Add(Commune commune)
        {
            _context.Communes.Add(commune);
            _context.SaveChanges();
        }
        public void Update(Commune commune)
        {
            _context.Communes.Update(commune);
            _context.SaveChanges();
        }

        public void Remove(Commune commune)
        {
            _context.Communes.Remove(commune);
            _context.SaveChanges();
        }

        public Commune Find(int id)
        {
            return _context.Communes.Include(d => d.District)
                .FirstOrDefault(x => x.Id == id)!;
        }

        public List<Commune> GetCommuneList()
        {
            return _context.Communes.ToList();
        }

        public List<Commune> GetCommuneListByPageNumber(int pageNumber)
        {
            var communes = _context.Communes.Skip((pageNumber-1)*Constant.Constant.PageSize)
                                            .Take(Constant.Constant.PageSize).ToList();
            foreach (var t in communes)
            {
                t.District = _districtRepo.GetDistrictById(t.DistrictId);
            }
            return communes;
        }

        public string? GetCommuneNameByCommuneId(int communeId)
        {
            return _context.Communes.Where(u => u.Id == communeId).Select(x => x.Name).FirstOrDefault();
        }

        public List<Commune> GetCommuneByDistrictId(int districtId)
        {
            return _context.Communes.Where(u => u.DistrictId == districtId).ToList();
        }

        public int GetCommuneCount()
        {
            return _context.Communes.Count();
        }
    }
}
