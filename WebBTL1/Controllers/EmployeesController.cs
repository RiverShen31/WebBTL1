using Microsoft.AspNetCore.Mvc;
using WebBTL1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Pagination;
using WebBTL1.ViewModels;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeeService;
        private readonly IEmployeesRepo _employeeRepo;
        private readonly IProvinceRepo _provinceRepo;

        public EmployeesController(IEmployeesRepo employeeRepo,
            IEmployeesService employeesService,
            IProvinceRepo provinceRepo)
        {
            _employeeService = employeesService;
            _employeeRepo = employeeRepo;
            _provinceRepo = provinceRepo;
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

            switch (Validate.ValidateEmployee(employee))
            {
                case "name":
                    ModelState.AddModelError("Name", "Contains only character");
                    return View(employee);
                case "dob":
                    ModelState.AddModelError("Dob",
                        $"Dob must from {Constant.Constant.MinYear} to {DateTime.Now.Year}");
                    return View(employee);
                case "phone":
                    ModelState.AddModelError("PhoneNumber", "Contains only number");
                    return View(employee);
                case "true":
                    break;
            }

            /*if (_employeeService.AddEmployee(_employeeService.SetEmployee(employee)))*/
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
            /*var employee = _employeeService.SetEmployeeViewModel(_employeeRepo.GetEmployeeById(id));*/
            var employee = _employeeRepo.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Dob,Ethnic,Job,IdentityNumber,PhoneNumber,Province,District,Commune, Description")]
            Employee employee)
        {
            DropDownList();
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            switch (Validate.ValidateEmployee(employee))
            {
                case "name":
                    ModelState.AddModelError("Name", "Contains only character");
                    return View(employee);
                case "dob":
                    ModelState.AddModelError("Dob",
                        $"Dob must from {Constant.Constant.MinYear} to {DateTime.Now.Year}");
                    return View(employee);
                case "phone":
                    ModelState.AddModelError("PhoneNumber", "Contains only number");
                    return View(employee);
                case "true":
                    break;
            }

            /*if (_employeeService.UpdateEmployee(_employeeService.SetEmployee(employee)))*/
            if (_employeeService.UpdateEmployee(employee))
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

        public void DropDownList()
        {
            ViewBag.EthnicList = new SelectList(_employeeRepo.GetEthnic(), "EthnicName", "EthnicName");
            ViewBag.JobList = new SelectList(_employeeRepo.GetJob(), "JobTitle", "JobTitle");
            /*ViewBag.ProvinceList = new SelectList(_provinceRepo.GetProvinceList(), "Id", "Name");*/
        }
    }
}