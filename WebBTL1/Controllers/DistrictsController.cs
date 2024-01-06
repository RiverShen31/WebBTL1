using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IDistrictRepo _districtRepo;
        private readonly IDistrictService _districtService;
        private readonly IProvinceRepo _provinceRepo;

        public DistrictsController(IDistrictService districtService,
            IDistrictRepo districtRepo,
            IProvinceRepo provinceRepo)
        {
            _districtService = districtService;
            _districtRepo = districtRepo;
            _provinceRepo = provinceRepo;
        }

        public JsonResult GetDistrictByProvinceId(int provinceId)
        {
            return Json(_districtRepo.GetDistrictByProvinceId(provinceId));
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(_districtService.PaginatedDistrict(pageNumber));
        }

        public IActionResult Details(int id)
        {
            return View(_districtRepo.Find(id));
        }

        public IActionResult Create()
        {
            DropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Level,ProvinceId")] District district)
        {
            if (!ModelState.IsValid)
            {
				DropDownList();
				return View(district);
            }
            _districtService.AddDistrict(district);
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Edit(int id)
        {
            DropDownList();
            return View(_districtRepo.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Level,ProvinceId")] District district)
        {
            DropDownList();
            if (!ModelState.IsValid) return View(district);
            _districtService.UpdateDistrict(district);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View(_districtRepo.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _districtService.DeleteDistrict(id);
            return RedirectToAction(nameof(Index));
        }

        public void DropDownList()
        {
            var districtLevel = new List<String> {
                "Thành phố",
                "Quận",
                "Huyện",
                "Thị xã"
            };
            ViewBag.LevelList = new SelectList(districtLevel);
            ViewData["ProvinceId"] = new SelectList(_provinceRepo.GetProvinceList(), "Id", "Name");
        }
    }
}
