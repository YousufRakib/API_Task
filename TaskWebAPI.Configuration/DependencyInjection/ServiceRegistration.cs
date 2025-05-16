using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Repository.IRepository;
using TaskWebAPI.Repository.Repository;
using TaskWebAPI.Service.IServices;
using TaskWebAPI.Service.Services;

namespace TaskWebAPI.Configuration.DependencyInjection
{
	public static class ServiceRegistration
	{
		public static void ConfigureServiceRegistration(this IServiceCollection services)
		{
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IAccountRepository, AccountRepository>();

			services.AddScoped<IOTPService, OTPService>();
			services.AddScoped<IOTPRepository, OTPRepository>();
		}
	}
}
