using WebBTL1.Data;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Repository
{
    public class EmployeesRepo : IEmployeesRepo
    {
        private readonly ApplicationDbContext _context;

        public EmployeesRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ethnic> GetEthnic()
        {
            return _context.Ethnices;
        }

        public IEnumerable<Job> GetJob()
        {
            return _context.Jobs;
        }

        public List<Province> GetProvinceList()
        {
            return _context.Provinces.ToList();
        }

        public List<District> GetDistrictList()
        {
            return _context.Districts.ToList();
        }

        public List<Commune> GetCommuneList()
        {
            return _context.Communes.ToList();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(m => m != null && m.Id == id);
        }

        public List<Employee?> GetEmployeesList()
        {
            return _context.Employees.ToList();
        }

        public List<Employee?> GetEmployeesListByPageNumber(int pageNumber)
        {
            return _context.Employees.Skip((pageNumber - 1) * Constant.Constant.PageSize)
                .Take(Constant.Constant.PageSize).ToList();
        }

        public void Insert(Employee? employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(int id, Employee? employee)
        {
            var entity = _context.Employees.FirstOrDefault(x => employee != null && x != null && x.Id == id);
            if (entity != null && employee != null)
            {
                entity.Name = employee.Name;
                entity.Dob = employee.Dob;
                entity.Age = employee.Age;
                entity.Ethnic = employee.Ethnic;
                entity.Job = employee.Job;
                entity.IdentityNumber = employee.IdentityNumber;
                entity.PhoneNumber = employee.PhoneNumber;
                _context.Employees.Update(entity);
            }

            _context.SaveChanges();
        }

        public bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e != null && e.Id == id)).GetValueOrDefault();
        }

        public void Remove(Employee? employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public int CountEmployee()
        {
            return _context.Employees.Count();
        }
    }
}