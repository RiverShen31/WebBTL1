using ClosedXML.Excel;
using System.Data;
using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;
using WebBTL1.Utils;
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
        if (!string.IsNullOrWhiteSpace(employee?.IdentityNumber))
        {
            return _employeesRepo.GetEmployeesList()
                    .Exists(e => e!.IdentityNumber == employee.IdentityNumber && e.Id != id);
        }
        return false;
    }

    public ImportResult ImportEmployees(IFormFile file)
    {
        if (file is not { Length: > 0 })
            return new ImportResult { Success = false, ErrorMessage = "Please choose another type of file" };
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        try
        {
            var workbook = new XLWorkbook(memoryStream);
            var provinceList = _employeesRepo.GetProvinceList();
            var districtList = _employeesRepo.GetDistrictList();
            var communeList = _employeesRepo.GetCommuneList();

            var response = ExcelFileManipulation.ImportEmployee(workbook, provinceList, districtList, communeList);

            if (response.ErrorMessage != null || response.Employee.Count < 1)
            {
                return new ImportResult
                {
                    Success = false,
                    ErrorMessage = $"Employee imported fail from {file.FileName}: \n" + response.ErrorMessage?.Content
                };
            }
            var count = 0;
            foreach (var employee in response.Employee)
            {
                count++;
                if (!AddEmployee(employee))
                {
                    return new ImportResult
                    {
                        Success = false,
                        ErrorMessage = $"Identity number exist in line {count + 1}"
                    };
                }
            }
            return new ImportResult { Success = true };
        }
        catch (Exception)
        {
            return new ImportResult { Success = false, ErrorMessage = "Please choose another type of file" };
        }
        return new ImportResult { Success = false, ErrorMessage = "Please choose another type of file" };
    }

    public byte[] ExportEmployeesToExcel()
    {
        var employees = SetEmployeeViewModelList(_employeesRepo.GetEmployeesList());
        return GenerateExcel(employees);
    }

    private static byte[] GenerateExcel(IEnumerable<EmployeeViewModel> employees)
    {
        var dataTable = new DataTable("Employees");
        dataTable.Columns.AddRange(new[]
        {
            new DataColumn("Id"),
            new DataColumn("Name"),
            new DataColumn("Dob"),
            new DataColumn("Age"),
            new DataColumn("Ethnic"),
            new DataColumn("Job"),
            new DataColumn("IdentityNumber"),
            new DataColumn("PhoneNumber"),
            new DataColumn("Province"),
            new DataColumn("District"),
            new DataColumn("Commune"),
            new DataColumn("Description")
        });

        foreach (var person in employees)
        {
            dataTable.Rows.Add(person.Id, person.Name, person.Dob, person.Age, person.Ethnic,
                person.Job, person.IdentityNumber, person.PhoneNumber, person.Province,
                person.District, person.Commune, person.Description);
        }

        using var wb = new XLWorkbook();
        wb.Worksheets.Add(dataTable);
        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }
}