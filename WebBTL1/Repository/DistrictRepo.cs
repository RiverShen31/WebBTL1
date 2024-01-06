using Microsoft.EntityFrameworkCore;
using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class DistrictRepo : IDistrictRepo
    {
        private readonly ApplicationDbContext _context;

        private readonly IProvinceRepo _provinceRepo;
        
        public DistrictRepo(ApplicationDbContext context,
                    IProvinceRepo provinceRepo)
        {
            _context = context;
            _provinceRepo = provinceRepo;
        }

        public void Add(District district)
        {
            _context.Districts.Add(district);
            _context.SaveChanges();
        }
        public void Update(District district)
        {
            _context.Districts.Update(district);
            _context.SaveChanges();
        }

        public void Remove(District district)
        {
            _context.Districts.Remove(district);
            _context.SaveChanges();
        }

        public District Find(int id)
        {
            return _context.Districts.Include(d => d.Province).FirstOrDefault(x => x.Id == id);
        }

        public List<District> GetDistrictList()
        {
            return _context.Districts.ToList();
        }

        public List<District> GetDistrictListByPageNumber(int pageNumber)
        {
            var districts = _context.Districts.Skip((pageNumber-1) * Constant.Constant.PageSize)
                                            .Take(Constant.Constant.PageSize).ToList();
            foreach (var t in districts)
            {
                t.Province = _provinceRepo.GetProvinceById(t.ProvinceId);
            }
            return districts;
        }

        public string? GetDistrictNameByDistrictId(int districtId)
        {
            return _context.Districts.Where(u => u.Id == districtId).Select(x => x.Name).FirstOrDefault();
        }

        public List<District> GetDistrictByProvinceId(int provinceId)
        {
            return _context.Districts.Where(u => u.ProvinceId == provinceId).ToList();
        }

        public District GetDistrictById(int id)
        {
            return _context.Districts.FirstOrDefault(m => m.Id == id);
        }

        public int GetDistrictCount()
        {
            return _context.Districts.Count();
        }
    }
}
