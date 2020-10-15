using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}


		public DbSet<StockItem> StockItems { get; set; }
		public DbSet<WareHouse> WareHouses { get; set; }
		public DbSet<Employee> Employees { get; set; }
	}
}
