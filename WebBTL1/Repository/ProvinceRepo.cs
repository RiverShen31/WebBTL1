using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class ProvinceRepo : IProvinceRepo
    {
        private readonly ApplicationDbContext _context;

        public ProvinceRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Province province)
        {
            _context.Provinces.Add(province);
            _context.SaveChanges();
        }

        public void Update(Province province)
        {
            _context.Provinces.Update(province);
            _context.SaveChanges();
        }

        public void Remove(Province province)
        {
            _context.Provinces.Remove(province);
            _context.SaveChanges();
        }

        public Province Find(int id)
        {
            return _context.Provinces.Find(id)!;
        }

        public IEnumerable<Province> GetProvinceList()
        {
            return _context.Provinces.ToList();
        }

        public IEnumerable<Province> GetProvinceListByPageNumber(int pageNumber)
        {
            return _context.Provinces.Skip((pageNumber -1) *  Constant.Constant.PageSize)
                                        .Take(Constant.Constant.PageSize).ToList();
        }

        public string? GetProvinceNameByProvinceId(int provinceId)
        {
            return _context.Provinces.Where(u => u.Id == provinceId).Select(x => x.Name).FirstOrDefault();
        }

        public Province? GetProvinceById(int provinceId)
        {
            return _context.Provinces.FirstOrDefault(m => m.Id == provinceId);
        }

        public int GetProvinceCount()
        {
            return _context.Provinces.Count();
        }
    }
}
