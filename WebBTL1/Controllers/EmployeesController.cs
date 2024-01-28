using Microsoft.AspNetCore.Mvc;
using WebBTL1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;
using ClosedXML.Excel;
using WebBTL1.Utils;
using System.Data;
using WebBTL1.ViewModels;

namespace WebBTL1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeeService;
        private readonly IEmployeesRepo _employeeRepo;

        public EmployeesController(IEmployeesRepo employeeRepo,
            IEmployeesService employeesService)
        {
            _employeeService = employeesService;
            _employeeRepo = employeeRepo;
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_employeeService.PaginatedEmployeeViewModel(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_employeeService.SetEmployeeViewModel(_employeeRepo.GetEmployeeById(id)));
        }

        public IActionResult Create()
        {
            DropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Dob,Ethnic,Job,IdentityNumber,PhoneNumber,Province,District,Commune,Description")]
            Employee employee)
        {
            DropDownList();
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var validator = new EmployeeValidator();
            var validationResult = validator.Validate(employee);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(employee);
            }

            if (_employeeService.AddEmployee(employee))
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("IdentityNumber",
                "Identity Number Already Exists.");
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            DropDownList();
            var employee = _employeeRepo.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            DropDownList();
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

			var validator = new EmployeeValidator();
			var validationResult = validator.Validate(employee);
			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(employee);
			}

            if (_employeeService.UpdateEmployee(id, employee))
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("IdentityNumber",
                "Identity Number Already Exists.");
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            return View(_employeeService.SetEmployeeViewModel(_employeeRepo.GetEmployeeById(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeService.DeleteEmployee(id);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file is { Length: > 0})
            {
                var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                try
                {
                    var workbook = new XLWorkbook(memoryStream);
                    var provinceList = _employeeRepo.GetProvinceList();
                    var districtList = _employeeRepo.GetDistrictList();
                    var communeList = _employeeRepo.GetCommuneList();

                    var response = ExcelFileManipulation.ImportEmployee(workbook, provinceList, districtList, communeList);

                    if (response.ErrorMessage != null || response.Employee.Count < 1)
                    {
                        TempData["error"] = $"Employee imported fail from {file.FileName}: \n" + response.ErrorMessage?.Content;
                        return RedirectToAction(nameof(Index));
                    }

                    var count = 0;
                    foreach (var employee in response.Employee)
                    {
                        count++;
                        if (!_employeeService.AddEmployee(employee))
                        {
                            TempData["error"] = $"Identity number exist in line {count+1}";
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    /*if (response.Employee.Any(employee => !_employeeService.AddEmployee(employee)))
                    {
                        TempData["error"] = "Identity number exists!";
                        return RedirectToAction(nameof(Index));
                    }*/

                    TempData["success"] = "Employee imported successfully from " + file.FileName;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["error"] = "Please choose other type of file";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Please choose other type of file";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public FileResult ExportEmployeeToExcel()
        {
            return GenerateExcel(Constant.Constant.FileNameEmployee, 
                _employeeService.SetEmployeeViewModelList(_employeeRepo.GetEmployeesList()));
        }

        public FileResult GenerateExcel(string fileName, IEnumerable<EmployeeViewModel> employees)
        {
            DataTable dataTable = new DataTable("Employees");
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
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        public void DropDownList()
        {
            ViewBag.EthnicList = new SelectList(_employeeRepo.GetEthnic(), "EthnicName", "EthnicName");
            ViewBag.JobList = new SelectList(_employeeRepo.GetJob(), "JobTitle", "JobTitle");
        }
    }
}