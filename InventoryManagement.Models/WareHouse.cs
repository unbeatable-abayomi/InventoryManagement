using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class WareHouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Display(Name = "WareHouse Address")]
        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }


    }
}
