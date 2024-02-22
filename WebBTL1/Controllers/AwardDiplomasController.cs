using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Models;
using WebBTL1.Repository;
using WebBTL1.Repository.Interface;
using WebBTL1.Services;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;

namespace WebBTL1.Controllers
{
    public class AwardDiplomasController : Controller
    {
        private readonly IAwardDiplomaRepo _awardDiplomaRepo;
        private readonly IAwardDiplomaService _awardDiplomaService;
        private readonly IEmployeesRepo _employeesRepo;
        private readonly IDiplomaRepo _diplomaRepo;
        private readonly IProvinceRepo _provinceRepo;

        public AwardDiplomasController(IAwardDiplomaRepo awardDiplomaRepo, IAwardDiplomaService awardDiplomaService,
                                       IEmployeesRepo employeesRepo, IDiplomaRepo diplomaRepo,
                                       IProvinceRepo provinceRepo)
        {
            _awardDiplomaRepo = awardDiplomaRepo;
            _awardDiplomaService = awardDiplomaService;
            _employeesRepo = employeesRepo;
            _diplomaRepo = diplomaRepo;
            _provinceRepo = provinceRepo;
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_awardDiplomaService.PaginatedAwardDiplomaViewModel(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_awardDiplomaService.SetAwardDiplomaViewModel(_awardDiplomaRepo.GetAwardDiplomaById(id)));
        }

        public IActionResult Create()
        {
            DropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, EmployeeId, DiplomaId, DiplomaGrantingUnitId, GrantingDate, Duration")] AwardDiploma awardDiploma)
        {
            DropDownList();

            var validator = new AwardDiplomaValidator(_awardDiplomaRepo);
            var validationResult = validator.Validate(awardDiploma);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(awardDiploma);
            }
            _awardDiplomaService.AddAwardDiploma(awardDiploma);
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            DropDownList();
            return View(_awardDiplomaRepo.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, EmployeeId, DiplomaId, DiplomaGrantingUnitId, GrantingDate, Duration")] AwardDiploma awardDiploma)
        {
            DropDownList();
            var validator = new AwardDiplomaValidator(_awardDiplomaRepo);
            var validationResult = validator.Validate(awardDiploma);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(awardDiploma);
            }

            _awardDiplomaService.UpdateAwardDiploma(awardDiploma);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

		public IActionResult Delete(int id)
		{
			return View(_awardDiplomaService.SetAwardDiplomaViewModel(_awardDiplomaRepo.GetAwardDiplomaById(id)));
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_awardDiplomaService.DeleteAwardDiploma(id);
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction(nameof(Index));
		}

		private void DropDownList()
        {
            ViewBag.EmployeeList = new SelectList(_employeesRepo.GetEmployeesList(), "Id", "Name");
            ViewBag.DiplomaList = new SelectList(_diplomaRepo.GetDiplomaList(), "Id", "Name");
            ViewBag.ProvinceList = new SelectList(_provinceRepo.GetProvinceList(), "Id", "Name");
        }
    }
}
