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
    public class ItemRepository : Repository<Item>, IItemRepository
	{
		private readonly ApplicationDbContext _db;
		public ItemRepository(ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(Item item)
		{
			var objFromDb = _db.Items.FirstOrDefault(i => i.Id == item.Id);

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
				objFromDb.Count = item.Count;



			};
		}
	}
}
