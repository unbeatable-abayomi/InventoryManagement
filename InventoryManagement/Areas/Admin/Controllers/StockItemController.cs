using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StockItemController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public StockItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}



		public IActionResult Index()
		{
			return View();
		}



		[HttpGet]
		public IActionResult Upsert(int? id)
		{
			StockItemVM stockItemVM = new StockItemVM()
			{
				StockItem = new Models.StockItem(),
				EmployeeList = _unitOfWork.Employee.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				}),
				WareHouseList = _unitOfWork.WareHouse.GetAll().Select(i => new SelectListItem
				{
					Text = i.Title,
					Value = i.Id.ToString()
				})

			};
			if (id == null)
			{
				return View(stockItemVM);
			}
			stockItemVM.StockItem = _unitOfWork.StockItem.Get(id.GetValueOrDefault());
			if (stockItemVM.StockItem == null)
			{
				return NotFound();
			}
			return View(stockItemVM);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(StockItemVM stockItemVM)
		{
			if (ModelState.IsValid)
			{
				string webRootPath = _hostEnvironment.WebRootPath;
				var files = HttpContext.Request.Form.Files;
				if (files.Count > 0)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(webRootPath, @"images\items");
					var extension = Path.GetExtension(files[0].FileName);
					if (stockItemVM.StockItem.ImageUrl != null)
					{
						//this is an edit we have to remove old image

						var imagePath = Path.Combine(webRootPath, stockItemVM.StockItem.ImageUrl.Trim('\\'));

						if (System.IO.File.Exists(imagePath))
						{

							System.IO.File.Delete(imagePath);
						}

					}

					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						files[0].CopyTo(fileStreams);
					}
					stockItemVM.StockItem.ImageUrl = @"\images\items\" + fileName + extension;
				}
				else
				{
					//update when they don't change the image

					if (stockItemVM.StockItem.Id != 0)
					{
						StockItem objFromDb = _unitOfWork.StockItem.Get(stockItemVM.StockItem.Id);
						stockItemVM.StockItem.ImageUrl = objFromDb.ImageUrl;
					}
				}
				if (stockItemVM.StockItem.Id == 0)
				{
					_unitOfWork.StockItem.Add(stockItemVM.StockItem);
				}
				else
				{
					_unitOfWork.StockItem.Update(stockItemVM.StockItem);
				}
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));

			}
			else
			{
				stockItemVM.EmployeeList = _unitOfWork.Employee.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				});

				stockItemVM.WareHouseList = _unitOfWork.WareHouse.GetAll().Select(i => new SelectListItem
				{
					Text = i.Title,
					Value = i.Id.ToString()
				});

				if (stockItemVM.StockItem.Id != 0)
				{
					stockItemVM.StockItem = _unitOfWork.StockItem.Get(stockItemVM.StockItem.Id);
				}
			}
			return View(stockItemVM.StockItem);
		}


		#region APICALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var allObj = _unitOfWork.StockItem.GetAll(includeProperties: "Employee,WareHouse");
			return Json(new { data = allObj });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var objFromDb = _unitOfWork.StockItem.Get(id);

			if (objFromDb == null)
			{
				return Json(new { success = false, message = "Error While deleting" });
			}
			string webRootPath = _hostEnvironment.WebRootPath;
			var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(imagePath))
			{
				System.IO.File.Delete(imagePath);
			}
			_unitOfWork.StockItem.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Succesfully Deleted" });

		}


		#endregion
	}
}
