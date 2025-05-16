using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebAPI.Models.EntityModel;

namespace TaskWebAPI.Models.DBContext
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> User { get; set; }
		public DbSet<OTPinfo> OTPinfo { get; set; }
		public DbSet<ErrorLog> ErrorLog { get; set; }
	}
}
