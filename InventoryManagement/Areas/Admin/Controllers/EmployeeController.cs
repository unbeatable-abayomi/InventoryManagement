using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class EmployeeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		private readonly IWebHostEnvironment _hostEnvironment;

		public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
		{ ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female", "Prefer Not to Say" });
			Employee employee = new Employee();

			if (id == null)
			{
				return View(employee);
			}
			employee = _unitOfWork.Employee.Get(id.GetValueOrDefault());

			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(Employee employee)
		{
			 ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female", "Prefer Not to Say" });
			if (ModelState.IsValid)
			{
				string webRootPath = _hostEnvironment.WebRootPath;
				var files = HttpContext.Request.Form.Files;
				if (files.Count > 0)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(webRootPath, @"images\employees");
					var extension = Path.GetExtension(files[0].FileName);
					if (employee.ImageUrl != null)
					{
						//this is an edit we have to remove old image

						var imagePath = Path.Combine(webRootPath, employee.ImageUrl.Trim('\\'));

						if (System.IO.File.Exists(imagePath))
						{

							System.IO.File.Delete(imagePath);
						}

					}

					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						files[0].CopyTo(fileStreams);
					}
					employee.ImageUrl = @"\images\employees\" + fileName + extension;
				}
				else
				{
					//update when they don't change the image

					if (employee.Id != 0)
					{
						Employee objFromDb = _unitOfWork.Employee.Get(employee.Id);
						employee.ImageUrl = objFromDb.ImageUrl;
					}
				}
				if (employee.Id == 0)
				{
					_unitOfWork.Employee.Add(employee);
				}
				else
				{
					_unitOfWork.Employee.Update(employee);
				}
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));

			}
			else
			{


				if (employee.Id != 0)
				{
					employee = _unitOfWork.Employee.Get(employee.Id);
				}
			}
			return View(employee);
		}


		#region APICALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var allObj = _unitOfWork.Employee.GetAll();
			return Json(new { data = allObj });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var objFromDb = _unitOfWork.Employee.Get(id);

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
			_unitOfWork.Employee.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Succesfully Deleted" });

		}


		#endregion

	}
}
