using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;

namespace WebBTL1.Controllers
{
    public class ProvincesController : Controller
    {

        private readonly IProvinceRepo _provinceRepo;
        private readonly IProvinceService _provinceService;

        public ProvincesController(IProvinceService provinceService, 
                                    IProvinceRepo provinceRepo)
        {
            _provinceService = provinceService;
            _provinceRepo = provinceRepo;
        }

        public JsonResult GetProvince()
        {
            return Json(_provinceRepo.GetProvinceList());
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_provinceService.PaginatedProvince(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_provinceRepo.GetProvinceById(id));
        }

        public IActionResult Create()
        {
            DropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Level")] Province province)
        {
            DropDownList();

			var validator = new ProvinceValidator();
			var validationResult = validator.Validate(province);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(province);
			}

			_provinceService.AddProvince(province);
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            DropDownList();
            return View(_provinceRepo.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Level")] Province province)
        {
            DropDownList();

			var validator = new ProvinceValidator();
			var validationResult = validator.Validate(province);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(province);
			}

			_provinceService.UpdateProvince(province);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View(_provinceRepo.GetProvinceById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _provinceService.DeleteProvince(id);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public void DropDownList()
        {
            var provinceLevel = new List<string> { 
                "Thành phố Trung ương",
                "Tỉnh"
            };
            ViewBag.LevelList = new SelectList(provinceLevel);
        }
    }
}
