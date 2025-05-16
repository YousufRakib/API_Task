using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.EntityModel;
using TaskWebAPI.Models.EnumModel;
using TaskWebAPI.Models.RequestModel;
using TaskWebAPI.Models.ResponseModel;
using TaskWebAPI.Repository.IRepository;
using TaskWebAPI.Repository.Repository;
using TaskWebAPI.Service.IServices;

namespace TaskWebAPI.Service.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<APIResponseObject<RegistrationResponseObjectDto>> SaveUserInfo(RegisterRequestDto model)
		{
			APIResponseObject<RegistrationResponseObjectDto> apiResponseObject = new();

			bool isValidGmail = true;

			isValidGmail = IsEmailAccountValid(model.Email);

			if (isValidGmail)
			{
				var isValidUser = await _accountRepository.IsExistUser(model.ICNumber);

				if (isValidUser.IsSuccess == true)
				{
					User registerUser = new();
					registerUser.UserId = Guid.NewGuid().ToString();
					registerUser.ICNumber = model.ICNumber;
					registerUser.FullName = model.FullName;
					registerUser.Email = model.Email;
					registerUser.MobileNumber = model.MobileNumber;
					registerUser.RegisterdDate = DateTime.Now;

					apiResponseObject = await _accountRepository.SaveUserInfo(registerUser);

					if (apiResponseObject.IsSuccess == true)
					{
						apiResponseObject.Result = new RegistrationResponseObjectDto{ UserId = registerUser.UserId, MobileNumber = registerUser.MobileNumber, Email = registerUser.Email };
					}
				}
				else
				{
					apiResponseObject = isValidUser;
				}
			}
			else
			{
				apiResponseObject = new(false, StatusCodes.Status400BadRequest, ResponseMessage.InvalidEmail);
			}
			return apiResponseObject;
		}

		public async Task<APIResponseObject<object>> CreatePIN(int PIN, string userId)
		{
			return await _accountRepository.CreatePIN(PIN, userId);
		}

		public async Task<APIResponseObject<RegistrationResponseObjectDto>> Login(LoginRequestDto model)
		{
			APIResponseObject<RegistrationResponseObjectDto> apiResponseObject = new();

			var isValidUser = await _accountRepository.IsExistUser(model.ICNumber);

			if (isValidUser.IsSuccess == true)
			{
				apiResponseObject.Result = isValidUser.Result;
			}
			else
			{
				apiResponseObject = isValidUser;
			}
			return apiResponseObject;
		}

		private bool IsEmailAccountValid(string emailAddress)
		{
			try
			{
				string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
				return Regex.IsMatch(emailAddress, pattern);
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
