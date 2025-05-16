using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.Dapper;
using TaskWebAPI.Models.DBContext;
using TaskWebAPI.Models.EntityModel;
using TaskWebAPI.Models.EnumModel;
using TaskWebAPI.Models.ResponseModel;
using TaskWebAPI.Repository.ErrorLog;
using TaskWebAPI.Repository.IRepository;

namespace TaskWebAPI.Repository.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private readonly string? _connectionString;
		private readonly ApplicationDbContext _db;
		private readonly IConfiguration? _configuration;
		private readonly ErrorLogRepository _errorLogRepository;
		private const string _repository = "AccountRepository";

		public AccountRepository(ApplicationDbContext db, IConfiguration? configuration)
		{
			_db = db;
			_configuration = configuration;
			_errorLogRepository = new ErrorLogRepository(_configuration);
			_connectionString = _configuration?.GetConnectionString("DefaultConnection");
		}

		public async Task<APIResponseObject<RegistrationResponseObjectDto>> SaveUserInfo(User model)
		{
			APIResponseObject<RegistrationResponseObjectDto> apiResponseObject = new();
			var errorCode = ErrorCode.GeneratedErrorCode();
			try
			{
				await _db.User.AddAsync(model);
				int result = await _db.SaveChangesAsync();
				if (result == 1)
				{
					apiResponseObject = new(true, StatusCodes.Status200OK, ResponseMessage.SavedData);
				}
				else
				{
					apiResponseObject = new(false, StatusCodes.Status400BadRequest, ResponseMessage.Error);
				}
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "SaveUserInfo", errorCode);
				apiResponseObject = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
			}
			return apiResponseObject;
		}

		public async Task<APIResponseObject<RegistrationResponseObjectDto>> IsExistUser(string ICNumber)
		{
			APIResponseObject<RegistrationResponseObjectDto> apiResponseObject = new();
			var errorCode = ErrorCode.GeneratedErrorCode();
			try
			{
				var isExistUser = await _db.User.Where(x => x.ICNumber == ICNumber).FirstOrDefaultAsync();

				if (isExistUser == null)
				{
					apiResponseObject = new(true, StatusCodes.Status200OK, ResponseMessage.UserNotFound);
				}
				else
				{
					apiResponseObject = new(false, StatusCodes.Status400BadRequest, ResponseMessage.UserAlreadyExist);
					apiResponseObject.Result = new RegistrationResponseObjectDto { UserId = isExistUser.UserId, MobileNumber = isExistUser.MobileNumber, Email = isExistUser.Email };
				}
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "IsExistUser", errorCode);
				apiResponseObject = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
			}
			return apiResponseObject;
		}

		public async Task<APIResponseObject<object>> CreatePIN(int PIN, string userId)
		{
			APIResponseObject<object> response = new();
			var errorCode = ErrorCode.GeneratedErrorCode();

			try
			{
				var isExistUser = await _db.User.Where(x => x.UserId == userId).FirstOrDefaultAsync();

				if (isExistUser == null)
				{
					//Insert PIN into User table in database
					using (var connection = new SqlConnection(_connectionString))
					{
						connection.Open();

						string query = string.Format(DapperQuery.DQR_InsertPIN);
						var resultDapper = await connection.QueryAsync<int>(query, new
						{
							PIN = PIN,
							UserId = userId
						});
					}

					response = new(true, StatusCodes.Status200OK, ResponseMessage.PINCreate);

					return response;
				}
				response = new(false, StatusCodes.Status404NotFound, ResponseMessage.UserNotFound);

				return response;
			}
			catch (Exception ex)
			{
				await _errorLogRepository.SaveErrorLog(ex.Message.ToString(), _repository, "CreatePIN", errorCode);
				response = new(false, StatusCodes.Status400BadRequest, ResponseMessage.ErrorMessage + errorCode);
				return response;
			}
		}
	}
}
