using Microsoft.AspNetCore.Mvc;
using WebBTL1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;

namespace WebBTL1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeeService;
        private readonly IEmployeesRepo _employeesRepo;

        public EmployeesController(IEmployeesRepo employeesRepo,
            IEmployeesService employeesService)
        {
            _employeeService = employeesService;
            _employeesRepo = employeesRepo;
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_employeeService.PaginatedEmployeeViewModel(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_employeeService.SetEmployeeViewModel(_employeesRepo.GetEmployeeById(id)));
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
            return View(_employeesRepo.GetEmployeeById(id));
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
            return View(_employeeService.SetEmployeeViewModel(_employeesRepo.GetEmployeeById(id)));
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
            var importResult = _employeeService.ImportEmployees(file);
            if (!importResult.Success)
            {
                TempData["error"] = importResult.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }
            TempData["success"] = "Employee imported successfully from " + file.FileName;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public FileResult ExportEmployeeToExcel()
        {
            var excelData = _employeeService.ExportEmployeesToExcel();
            return File(excelData, Constant.Constant.FileExcelContentType, Constant.Constant.FileNameEmployee);
        }

        public void DropDownList()
        {
            ViewBag.EthnicList = new SelectList(_employeesRepo.GetEthnic(), "EthnicName", "EthnicName");
            ViewBag.JobList = new SelectList(_employeesRepo.GetJob(), "JobTitle", "JobTitle");
        }
    }
}