using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.EntityModel;
using TaskWebAPI.Models.ResponseModel;

namespace TaskWebAPI.Repository.IRepository
{
	public interface IAccountRepository
	{
		Task<APIResponseObject<RegistrationResponseObjectDto>> IsExistUser(string ICNumber);
		Task<APIResponseObject<RegistrationResponseObjectDto>> SaveUserInfo(User model);
		Task<APIResponseObject<object>> CreatePIN(int PIN, string userId);
	}
}
