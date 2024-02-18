using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface IEmployeesRepo
    {
        IEnumerable<Ethnic> GetEthnic();

        IEnumerable<Job> GetJob();

        List<Province> GetProvinceList();

        List<District> GetDistrictList();

        List<Commune> GetCommuneList();

        Employee? GetEmployeeById(int id);

        List<Employee?> GetEmployeesList();

        List<Employee?> GetEmployeesListByPageNumber(int pageNumber);

        int CountEmployee();

        void Insert(Employee? employee);

        void Remove(Employee? employee);

        void Update(int id, Employee? employee);
    }
}
