using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class WareHouseController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public WareHouseController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public IActionResult Upsert(int? id)
		{
			WareHouse wareHouse = new WareHouse();

			if (id == null)
			{
				return View(wareHouse);
			}
			wareHouse = _unitOfWork.WareHouse.Get(id.GetValueOrDefault());

			if (wareHouse == null)
			{
				return NotFound();
			}
			return View(wareHouse);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(WareHouse wareHouse)
		{
			if (ModelState.IsValid)
			{
				if (wareHouse.Id == 0)
				{
					_unitOfWork.WareHouse.Add(wareHouse);
				}
				else
				{
					_unitOfWork.WareHouse.Update(wareHouse);
				}
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(wareHouse);
		}



		#region APICALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var allObj = _unitOfWork.WareHouse.GetAll();
			return Json(new { data = allObj });
		}


		[HttpDelete]

		public IActionResult Delete(int id)
		{
			var objFromDb = _unitOfWork.WareHouse.Get(id);
			if (objFromDb == null)
			{
				return Json(new { success = false, message = "Error While Deleting" });
			}
			_unitOfWork.WareHouse.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "SuccessFully Deleted" });
		}
		#endregion
	}
}
