using InventoryManagement.Data;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository
{
	public class StockItemRepository : Repository<StockItem>, IStockItemRepository
	{
		private readonly ApplicationDbContext _db;
		public StockItemRepository(ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(StockItem item)
		{
			var objFromDb = _db.StockItems.FirstOrDefault(i => i.Id == item.Id);

			if (objFromDb != null)
			{
				if (objFromDb.ImageUrl != null)
				{
					objFromDb.ImageUrl = item.ImageUrl;
				};
				objFromDb.Name = item.Name;
				objFromDb.Price = item.Price;
				objFromDb.WareHouseId = item.WareHouseId;
				objFromDb.EmployeeId = item.EmployeeId;
				objFromDb.Description = item.Description;
				objFromDb.Created = item.Created;
				objFromDb.Count = item.Count;



			};
		}
	}
}
