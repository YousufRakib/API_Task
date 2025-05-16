using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.RequestModel
{
	public class ValidationOTPRequest
	{
		public string UserId { get; set; }
		public int OTP { get; set; }
	}
}
