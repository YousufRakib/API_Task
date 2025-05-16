using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.CommonModel
{
	public static class ErrorCode
	{
		public static string GeneratedErrorCode()
		{
			string errorCode = "#" + DateTime.Now.ToString("yyyyMMddHHmmss");
			return errorCode;
		}
	}
}
