using Azure;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.Dapper;
using TaskWebAPI.Models.DBContext;
using TaskWebAPI.Models.EnumModel;
using TaskWebAPI.Repository.ErrorLog;
using TaskWebAPI.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskWebAPI.Repository.Repository
{
	public class OTPRepository : IOTPRepository
	{
		private readonly string? _connectionString;
		private readonly ApplicationDbContext _db;
		private readonly IConfiguration? _configuration;
		private readonly ErrorLogRepository _errorLogRepository;
		private const string _repository = "OTPRepository";

		public OTPRepository(ApplicationDbContext db, IConfiguration? configuration)
		{
			_db = db;
			_configuration = configuration;
			_errorLogRepository = new ErrorLogRepository(_configuration);
			_connectionString = _configuration?.GetConnectionString("DefaultConnection");
		}

		public async Task<APIResponseObject<object>> SendOTPToMobile(string userId, string mobileNumber)
		{
			APIResponseObject<object> response = new();
			var errorCode = ErrorCode.GeneratedErrorCode();
			try
			{
				var currentAspUser = await _db.User.FirstOrDefaultAsync(x => x.UserId == userId && x.MobileNumber == mobileNumber);

				if (currentAspUser != null)
				{
					string result = "";
					WebRequest request = null;
					HttpWebResponse httpWebResponse = null;

					//Generate OTP
					Random random = new Random();
					var otpLentgh = 4;
					const string chars = "0123456789";
					var genaratedOTP = new string(Enumerable.Repeat(chars, otpLentgh).Select(s => s[random.Next(s.Length)]).ToArray());

					//Insert OTP into OTPInfo table in database
					using (var connection = new SqlConnection(_connectionString))
					{
						connection.Open();

						string query = string.Format(DapperQuery.DQR_InsertOTP);
						var resultDapper = await connection.QueryAsync<int>(query, new
						{
							Id = Guid.NewGuid().ToString(),
							UserId = currentAspUser.UserId,
							MobileNumber = currentAspUser.MobileNumber,
							Email = "",
							OTP = genaratedOTP,
							CreatedDate = DateTime.Now,
							OTPStatus = Convert.ToInt32(OTPStatusEnum.Active)
						});
					}

					string generatedMessage = "Your OTP is: " + genaratedOTP + ". Please do not share your OTP with others.";
					string message = System.Uri.EscapeUriString(generatedMessage);

					var apiKey = "C300122365e6d41dee81a1.33xxxxx"; // This is not real apiKey
					var senderId = "+880960101xxxx"; // This is not real senderId
					string url = "https://api.mram.com.bd/smsapi?api_key=" + apiKey + "&type=text&contacts=" + mobileNumber + "&senderid=" + senderId + "&msg=" + message;

					request = WebRequest.Create(url);

					// Send the 'HttpWebRequest' and wait for response.
					httpWebResponse = (HttpWebResponse)request.GetResponse();
					Stream stream = httpWebResponse.GetResponseStream();
					Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
					StreamReader reader = new System.IO.StreamReader(stream, ec);
					result = reader.ReadToEnd();
					reader.Close();
					stream.Close();

					response = new(true, StatusCodes.Status200OK, ResponseMessage.OTPSend);
					//response.Result = new { UserId = currentAspUser.UserId, MobileNumber = currentAspUser.MobileNumber, Email = currentAspUser.Email }; ;
					
					return response;
				}

				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.Error);
				return response;
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "SendOTPtoPhone", errorCode);
				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
				return response;
			}
		}

		public async Task<APIResponseObject<object>> SendOTPToEmail(string userId, string email)
		{
			APIResponseObject<object> response = new();
			var errorCode = ErrorCode.GeneratedErrorCode();
			try
			{
				var currentAspUser = await _db.User.FirstOrDefaultAsync(x => x.UserId == userId && x.Email == email);

				if (currentAspUser != null)
				{
					string result = "";
					WebRequest request = null;
					HttpWebResponse httpWebResponse = null;

					//Generate OTP
					Random random = new Random();
					var otpLentgh = 4;
					const string chars = "0123456789";
					var genaratedOTP = new string(Enumerable.Repeat(chars, otpLentgh).Select(s => s[random.Next(s.Length)]).ToArray());

					//Insert OTP into OTPInfo table in database
					using (var connection = new SqlConnection(_connectionString))
					{
						connection.Open();

						string query = string.Format(DapperQuery.DQR_InsertOTP);
						var resultDapper = await connection.QueryAsync<int>(query, new
						{
							Id = Guid.NewGuid().ToString(),
							UserId = currentAspUser.UserId,
							MobileNumber = "",
							Email = currentAspUser.Email,
							OTP = genaratedOTP,
							CreatedDate = DateTime.Now,
							OTPStatus = Convert.ToInt32(OTPStatusEnum.Active)
						});
					}

					response = new(true, StatusCodes.Status200OK, ResponseMessage.OTPSend);
					//response.Result = new { OTP = genaratedOTP, UserId = currentAspUser.UserId, MobileNumber = currentAspUser.MobileNumber, Email = currentAspUser.Email }; ;

					return response;
				}

				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.Error);
				return response;
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "SendOTPtoEmail", errorCode);
				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
				return response;
			}
		}

		public async Task<APIResponseObject<object>> OTPValidationCheck(int OTP, string userId)
		{
			APIResponseObject<object> response = new();
			var errorCode = ErrorCode.GeneratedErrorCode();

			try
			{
				var isExistOTP = await _db.OTPinfo.Where(x => x.UserId == userId && x.OTP == OTP && x.OTPStatus == Convert.ToInt32(OTPStatusEnum.Active)).FirstOrDefaultAsync();

				if (isExistOTP == null)
				{
					//Update OTP Status into OTPInfo table in database
					using (var connection = new SqlConnection(_connectionString))
					{
						connection.Open();

						string query = string.Format(DapperQuery.DQR_UpdateOTPStatus);
						var resultDapper = await connection.QueryAsync<int>(query, new
						{
							OTPStatus = Convert.ToInt32(OTPStatusEnum.Used),
							UserId = userId,
							OTP = OTP
						});
					}

					response = new(true, StatusCodes.Status200OK, ResponseMessage.ValidOTP);
					
					return response;
				}
				response = new(false, StatusCodes.Status404NotFound, ResponseMessage.InValidOTP);

				return response;
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "OTPValidationCheck", errorCode);
				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
				return response;
			}
		}
	}
}
