using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.RequestModel
{
	public class RegisterRequestDto
	{
		public string FullName { get; set; }

		public string ICNumber { get; set; }

		public string MobileNumber { get; set; }

		public string Email { get; set; }
	}
}
