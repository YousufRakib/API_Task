using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.Dapper;
using TaskWebAPI.Models.DBContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskWebAPI.Repository.ErrorLog
{
	public class ErrorLogRepository
	{
		private readonly string? _connectionString;
		private readonly IConfiguration? _configuration;


		public ErrorLogRepository(IConfiguration? configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("DefaultConnection");
		}

		public async Task<bool> SaveErrorLog(string message, string repository, string function, string errorCode)
		{
			try
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();

					string query = string.Format(DapperQuery.DQR_InsertExceptionMessage);
					await connection.QueryAsync<int>(query, new
					{
						Message = message,
						Repository = repository,
						Function = function,
						CreatedDate = DateTime.Now,
                        ErrorCode = errorCode
					});
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
