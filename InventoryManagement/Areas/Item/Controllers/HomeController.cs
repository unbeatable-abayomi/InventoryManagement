using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryManagement.Models;
using InventoryManagement.DataAccess.Repository.IRepository;

namespace InventoryManagement.Areas.Item.Controllers
{
	[Area("Item")]
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<StockItem> stockItems = _unitOfWork.StockItem.GetAll(includeProperties: "Employee,WareHouse");

			return View(stockItems);
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			var stockItem = _unitOfWork.StockItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Employee,WareHouse");
			return View(stockItem);
		}

		public IActionResult Plus(int cartId)
		{
			var stock = _unitOfWork.StockItem.GetFirstOrDefault(c => c.Id == cartId, includeProperties: "WareHouse,Employee");
			stock.Count += 1;
			_unitOfWork.Save();
			return RedirectToAction(nameof(Details), new { id = stock.Id });
		}

		public IActionResult Minus(int cartId)
		{
			var stock = _unitOfWork.StockItem.GetFirstOrDefault(c => c.Id == cartId, includeProperties: "WareHouse,Employee");

			if (stock.Count == 1 || stock.Count == 0)
			{
				stock.Count = 0;

				_unitOfWork.Save();
		
			}
			else
			{

				stock.Count -= 1;
				
				_unitOfWork.Save();
			}

			return RedirectToAction(nameof(Details), new { id = stock.Id });
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
