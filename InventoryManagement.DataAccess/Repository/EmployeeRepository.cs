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
    public class EmployeeRepository : Repository<Employee>,IEmployeeRepository
    {
		private readonly ApplicationDbContext _db;
		public EmployeeRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}

		public void Update(Employee employee)
		{
			var objFromDb = _db.Employees.FirstOrDefault(i => i.Id == employee.Id);

			if (objFromDb != null)
			{
				if (objFromDb.ImageUrl != null)
				{
					objFromDb.ImageUrl = employee.ImageUrl;
				};
				objFromDb.Name = employee.Name;
				objFromDb.Age = employee.Age;
				objFromDb.Gender = employee.Gender;
				objFromDb.IdentityCardNumber = employee.IdentityCardNumber;
				objFromDb.PhoneNumber = employee.PhoneNumber;
				



			};
		}
	}
}
