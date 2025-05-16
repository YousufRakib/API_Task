using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.RequestModel;
using TaskWebAPI.Models.ResponseModel;

namespace TaskWebAPI.Service.IServices
{
	public interface IAccountService
	{
		Task<APIResponseObject<RegistrationResponseObjectDto>> SaveUserInfo(RegisterRequestDto model);
		Task<APIResponseObject<object>> CreatePIN(int PIN, string userId);
		Task<APIResponseObject<RegistrationResponseObjectDto>> Login(LoginRequestDto model);
	}
}
