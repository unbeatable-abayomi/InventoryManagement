using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository.IRepository
{
    public interface IWareHouseRepository : IRepository<WareHouse>
    {
        void Update(WareHouse wareHouse);
    }
}
