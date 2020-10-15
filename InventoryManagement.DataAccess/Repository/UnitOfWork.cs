using InventoryManagement.Data;
using InventoryManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
            _db = db;
			Item = new ItemRepository(_db);
			WareHouse = new WareHouseRepository(_db);
			Employee = new EmployeeRepository(_db);

		}

		public IItemRepository Item { get; private set; }
		public IWareHouseRepository WareHouse { get; private set; }
		public IEmployeeRepository Employee { get; private set; }


		public void Dispose()
		{
			_db.Dispose();
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
