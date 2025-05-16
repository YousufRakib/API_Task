using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.EntityModel
{
	public class User
	{
		[Key]
		public string UserId { get; set; } = Guid.NewGuid().ToString();
		
		public string FullName { get; set; }

		public string ICNumber { get; set; }

		public string MobileNumber { get; set; }

		public string Email { get; set; }

		public int PIN { get; set; }

		public DateTime RegisterdDate { get; set; }
	}
}
