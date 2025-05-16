using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.RequestModel
{
	public class PhoneOTPRequest
	{
		public string MobileNumber { get; set; }
		public string UserId { get; set; }
	}
}
