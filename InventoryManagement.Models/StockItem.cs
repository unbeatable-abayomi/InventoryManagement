using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class StockItem
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Display(Name = "Count Of Items To Stocked")]
        [Required]
       // [MaxLength(50)]
        public int Count { get; set; }
        public string ImageUrl { get; set; }


   

		public string DateCreated { get; set; } = DateTime.Now.ToShortDateString();

        [Display(Name = "What Warehouse are you Choosing to Stock Items?")]
        [Required]
        public int WareHouseId { get; set; }

        [ForeignKey("WareHouseId")]
        public WareHouse WareHouse { get; set; }


        [Display(Name = "Choose Employee To Capture the inventories")]
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
