using Microsoft.AspNetCore.Mvc;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;

namespace WebBTL1.Controllers
{
    public class DiplomasController : Controller
    {
        private readonly IDiplomaRepo _diplomaRepo;
        private readonly IDiplomaService _diplomaService;

        public DiplomasController(IDiplomaRepo diplomaRepo, IDiplomaService diplomaService)
        {
            _diplomaRepo = diplomaRepo;
            _diplomaService = diplomaService;
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_diplomaService.PaginatedDiploma(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_diplomaRepo.GetDiplomaById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name")] Diploma diploma)
        {
            var validator = new DiplomaValidator();
            var validationResult = validator.Validate(diploma);

            if (!validationResult.IsValid)
            {
                foreach (var error in  validationResult.Errors) 
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(diploma);
            }
            _diplomaService.AddDiploma(diploma);
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View(_diplomaRepo.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name")] Diploma diploma)
        {
            var validator = new DiplomaValidator();
            var validationResult = validator.Validate(diploma);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(diploma);
            }

            _diplomaService.UpdateDiploma(diploma);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View(_diplomaRepo.GetDiplomaById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _diplomaService.DeleteDiploma(id);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
