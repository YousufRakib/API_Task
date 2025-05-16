using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;

namespace TaskWebAPI.Service.IServices
{
	public interface IOTPService
	{
		Task<APIResponseObject<object>> SendOTPToMobile(string userId, string phone);
		Task<APIResponseObject<object>> SendOTPToEmail(string userId, string email);
		Task<APIResponseObject<object>> OTPValidationCheck(int OTP, string userId);
	}
}
