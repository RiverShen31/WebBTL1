using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;
using WebBTL1.ViewModels;

namespace WebBTL1.Services;

public class EmployeesService : IEmployeesService
{

    private readonly IEmployeesRepo _employeesRepo;
    private readonly IProvinceRepo _provinceRepo;
    private readonly IDistrictRepo _districtRepo;
    private readonly ICommuneRepo _communeRepo;

    public EmployeesService(IEmployeesRepo employeesRepo,
                            IProvinceRepo provinceRepo,
                            IDistrictRepo districtRepo,
                            ICommuneRepo communeRepo)
    {
        _employeesRepo = employeesRepo;
        _provinceRepo = provinceRepo;
        _districtRepo = districtRepo;
        _communeRepo = communeRepo;
    }

    public bool AddEmployee(Employee? employee)
    {
        if (employee == null) return true;
        employee.Age = Employee.SetAge(employee.Dob);
        if (CheckIdentityNumberDuplicate(employee, employee.Id)) return false;
        try
        {
            _employeesRepo.Insert(employee);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public bool UpdateEmployee(int id, Employee? employee)
    {
        if (employee == null) return false;
        employee.Age = Employee.SetAge(employee.Dob);
        if (CheckIdentityNumberDuplicate(employee, id)) return false;

        try
        {
            _employeesRepo.Update(id, employee);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public void DeleteEmployee(int id)
    {
        var employee = _employeesRepo.GetEmployeeById(id);
        if (employee != null)
        {
            _employeesRepo.Remove(employee);
        }
    }

    public EmployeeViewModel SetEmployeeViewModel(Employee? employee)
    {
        return new EmployeeViewModel(employee!.Id, employee.Name, employee.Dob, employee.Age,
                                   employee.Ethnic, employee.Job, employee.IdentityNumber,
                                   employee.PhoneNumber, 
                                   employee.Province,
                                   _provinceRepo.GetProvinceNameByProvinceId(employee.Province),
                                   employee.District,
                                   _districtRepo.GetDistrictNameByDistrictId(employee.District),
                                   employee.Commune,
                                   _communeRepo.GetCommuneNameByCommuneId(employee.Commune),
                                   employee.Description);
    }

    public List<EmployeeViewModel> SetEmployeeViewModelList(List<Employee?> employees)
    {
        return employees.Select(SetEmployeeViewModel).ToList();
    }

    public PaginatedList<EmployeeViewModel> PaginatedEmployeeViewModel(int? pageNumber)
    {
        return PaginatedList<EmployeeViewModel>.Create(SetEmployeeViewModelList
					(_employeesRepo.GetEmployeesListByPageNumber(Validate.ValidatePageNumber(ref pageNumber))),
				_employeesRepo.CountEmployee()
				, Validate.ValidatePageNumber(ref pageNumber), Constant.Constant.PageSize);
    }


    private bool CheckIdentityNumberDuplicate(Employee? employee, int id)
    {
        if (employee?.IdentityNumber != null)
        {
            return _employeesRepo.GetEmployeesList()
                    .Exists(e => e!.IdentityNumber == employee.IdentityNumber && e.Id != id);
        }
        return false;
    }
}