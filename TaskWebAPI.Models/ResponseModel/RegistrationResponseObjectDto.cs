using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.ResponseModel
{
	public class RegistrationResponseObjectDto
	{
		public string UserId { get; set; }

		public string MobileNumber { get; set; }

		public string Email { get; set; }
	}
}
