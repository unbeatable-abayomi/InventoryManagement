using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public enum Gender
	{
		
		Male,
		Female
	}
    public class Employee
    {

		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Display(Name = "ID No")]
		[Required]
		[MaxLength(50)]
		public string IdentityCardNumber { get; set; }
		public int Age { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public Gender Gender { get; set; }

		public string ImageUrl { get; set; }
	}
}
