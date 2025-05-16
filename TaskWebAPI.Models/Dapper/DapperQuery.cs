using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.Dapper
{
    public static class DapperQuery
    {
		public const string DQR_InsertExceptionMessage = "Insert Into ErrorLogs (Id, Message, Repository, Function, CreatedDate, ErrorCode) Values (@Id, @Message, @Repository, @Function, @CreatedDate, @ErrorCode)";
        public const string DQR_InsertOTP = "INSERT INTO OTPInfo (Id, UserId, MobileNumber, Email, OTP, CreatedDate, OTPStatus) values (@Id, @UserId, @MobileNumber, @Email, @OTP, @CreatedDate, @OTPStatus)";
        public const string DQR_UpdateOTPStatus = "UPDATE OTPInfo Set OTPStatus = @OTPStatus Where UserId = @UserId And OTP = @OTP;";
        public const string DQR_InsertPIN = "UPDATE User Set PIN = @PIN Where UserId = @UserId";
    }
}
