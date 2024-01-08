using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.ViewModels;

namespace WebBTL1.Services.Interface
{
    public interface IEmployeesService
    {
        bool AddEmployee(Employee? employee);

        bool UpdateEmployee(int id, Employee? employee);

        void DeleteEmployee(int id);

        EmployeeViewModel SetEmployeeViewModel(Employee? employee);

        List<EmployeeViewModel> SetEmployeeViewModelList(List<Employee?> employees);

        PaginatedList<EmployeeViewModel> PaginatedEmployeeViewModel(int? pageNumber);
    }
}
