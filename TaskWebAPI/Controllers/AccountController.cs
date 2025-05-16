using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using TaskWebAPI.Configuration.Halper;
using TaskWebAPI.Models.RequestModel;
using TaskWebAPI.Service.IServices;

namespace TaskWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IOTPService _OTPService;

		public AccountController(IAccountService accountService, IOTPService OTPService) 
		{
			_accountService = accountService;
			_OTPService = OTPService;
		}

		[AllowAnonymous]
		[HttpPost("~/User/Register")]
		public async Task<IActionResult> Register(RegisterRequestDto model)
		{
			var response = await _accountService.SaveUserInfo(model);
			if (response.IsSuccess)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[AllowAnonymous]
		[HttpPost("~/User/GenerateSendOTPToMobile")]
		public async Task<IActionResult> GenerateSendOTPToMobile(PhoneOTPRequest model)
		{
			var otpSendResponse = _OTPService.SendOTPToMobile(model.UserId, model.MobileNumber);

			if (otpSendResponse.Result.IsSuccess)
			{
				return Ok(otpSendResponse);
			}
			return BadRequest(otpSendResponse);
		}

		[AllowAnonymous]
		[HttpPost("~/User/GenerateSendOTPToEmail")]
		public async Task<IActionResult> GenerateSendOTPToEmail(EmailOTPRequest model)
		{
			var otpSendResponse = _OTPService.SendOTPToEmail(model.UserId, model.Email);

			if (otpSendResponse.Result.IsSuccess)
			{
				return Ok(otpSendResponse);
			}
			return BadRequest(otpSendResponse);
		}

		[AllowAnonymous]
		[HttpPost("~/User/ValidationOTP")]
		public async Task<IActionResult> ValidationOTP(ValidationOTPRequest model)
		{
			var response = await _OTPService.OTPValidationCheck(model.OTP, model.UserId);
			if (response.IsSuccess)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[AllowAnonymous]
		[HttpPost("~/User/CreatePIN")]
		public async Task<IActionResult> CreatePIN(CreatePINRequest model)
		{
			var response = await _accountService.CreatePIN(model.PIN, model.UserId);
			if (response.IsSuccess)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[AllowAnonymous]
		[HttpPost("~/User/Login")]
		public async Task<IActionResult> Login(LoginRequestDto model)
		{
			var response = await _accountService.Login(model);
			if (response.IsSuccess)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}
	}
}
