using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModels
{
    public class StockItemVM
    {
        public StockItem StockItem { get; set; }

        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> WareHouseList { get; set; }
    }
}
