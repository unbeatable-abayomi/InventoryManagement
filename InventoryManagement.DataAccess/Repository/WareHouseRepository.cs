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
    public class WareHouseRepository : Repository<WareHouse>, IWareHouseRepository
    {
		private readonly ApplicationDbContext _db;
		public WareHouseRepository(ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(WareHouse wareHouse)
		{
			_db.Update(wareHouse);
		}
	}
}
