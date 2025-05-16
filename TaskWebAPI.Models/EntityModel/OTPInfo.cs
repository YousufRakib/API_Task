using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.EntityModel
{
	public class OTPinfo
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string UserId { get; set; }

		public string? MobileNumber { get; set; }

		public string? Email { get; set; }

		public int OTP { get; set; }

		public DateTime CreatedDate { get; set; }

		public int OTPStatus { get; set; }
	}
}
