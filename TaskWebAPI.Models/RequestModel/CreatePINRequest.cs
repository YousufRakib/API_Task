using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.RequestModel
{
	public class CreatePINRequest
	{
		public int PIN { get; set; }
		public string UserId { get; set; }
	}
}
