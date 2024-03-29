﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Validators;

namespace WebBTL1.Controllers
{
	public class CommunesController : Controller
	{
		private readonly ICommuneRepo _communeRepo;
		private readonly ICommuneService _communeService;
		private readonly IDistrictRepo _districtRepo;

		public CommunesController(ICommuneService communeService,
					ICommuneRepo communeRepo, IDistrictRepo districtRepo)
		{
			_communeService = communeService;
			_communeRepo = communeRepo;
			_districtRepo = districtRepo;
		}

		public JsonResult GetCommuneByDistrictId(int districtId)
		{
			return Json(_communeRepo.GetCommuneByDistrictId(districtId));
		}

		public IActionResult Index(int? pageNumber)
		{
			return View(_communeService.PaginatedCommune(pageNumber));
		}

		public IActionResult Details(int id)
		{
			return View(_communeRepo.Find(id));
		}

		public IActionResult Create()
		{
			DropDownList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Name,Level,DistrictId")] Commune commune)
		{
			DropDownList();

			var validator = new CommuneValidator();
			var validationResult = validator.Validate(commune);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(commune);
			}

			_communeService.AddCommune(commune);
			TempData["success"] = "Category created successfully";
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			DropDownList();
			return View(_communeRepo.Find(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Name,Level,DistrictId")] Commune commune)
		{
			DropDownList();

			var validator = new CommuneValidator();
			var validationResult = validator.Validate(commune);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(commune);
			}

			_communeService.UpdateCommune(commune);
			TempData["success"] = "Category updated successfully";
			return RedirectToAction(nameof(Index));

		}

		public IActionResult Delete(int id)
		{
			return View(_communeRepo.Find(id));
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_communeService.DeleteCommune(id);
			return RedirectToAction(nameof(Index));
		}
		
		public void DropDownList()
		{
			var communeLevel = new List<string>
			{
				"Phường",
				"Xã",
				"Thị trấn"
			};
			ViewBag.LevelList = new SelectList(communeLevel);
			ViewData["DistrictId"] = new SelectList(_districtRepo.GetDistrictList(), "Id", "Name");
		}
	}
}
