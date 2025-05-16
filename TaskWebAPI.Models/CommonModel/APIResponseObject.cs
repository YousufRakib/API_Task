using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.CommonModel
{
	public class APIResponseObject<T>
	{
		public T Result { get; set; }
		public bool IsSuccess { get; set; }
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public APIResponseObject(T result, bool isSuccess = false, int statusCode = 0, string message = "")
		{
			Result = result;
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			Message = message;
		}

		public APIResponseObject(bool isSuccess = false, int statusCode = 0, string message = "")
		{
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			Message = message;
		}
	}
}
