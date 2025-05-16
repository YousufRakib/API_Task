using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.CommonModel
{
	public static class ResponseMessage
	{
		public static string ErrorMessage = "Oops! Something went wrong. Please contact our support team (Error Code: {0}).";
		public static string UserNotFound = "This user account doesn't exist in our system.";
		public static string InvalidEmail = "Please enter a valid email address.";
		public static string Error = "Oops! Something went wrong.!!";
		public static string SavedData = "Data saved successfully!!";
		public static string UserAlreadyExist = "This user already exists in our system.";
		public static string OTPSend = "We've sent an OTP to your phone.";
		public static string OTPSendToEmail = "Check your email for the OTP. It's valid for 3 minutes!";
		public static string ValidOTP = "OTP is valid!";
		public static string InValidOTP = "InValid OTP!";
		public static string PINCreate = "PIN Created!";
	}
}
