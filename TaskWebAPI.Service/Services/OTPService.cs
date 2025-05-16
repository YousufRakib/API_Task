using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Repository.IRepository;
using TaskWebAPI.Service.IServices;

namespace TaskWebAPI.Service.Services
{
	public class OTPService : IOTPService
	{
		private readonly IOTPRepository _OTPRepository;
		public OTPService(IOTPRepository OTPRepository)
		{
			_OTPRepository = OTPRepository;
		}

		public async Task<APIResponseObject<object>> SendOTPToMobile(string userId, string phone)
		{
			return await _OTPRepository.SendOTPToMobile(userId, phone);
		}

		public async Task<APIResponseObject<object>> SendOTPToEmail(string userId, string email)
		{
			return await _OTPRepository.SendOTPToEmail(userId, email);
		}

		public async Task<APIResponseObject<object>> OTPValidationCheck(int OTP, string userId)
		{
			return await _OTPRepository.OTPValidationCheck(OTP, userId);
		}
	}
}
