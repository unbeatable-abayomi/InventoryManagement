using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IStockItemRepository StockItem { get; }
        IWareHouseRepository WareHouse { get; }
        IEmployeeRepository Employee { get; }

        void Save();
    }
}
