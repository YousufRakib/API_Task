using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskWebAPI.Configuration.DependencyInjection;
using TaskWebAPI.Models.CommonModel;
using TaskWebAPI.Models.DBContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

//builder.WebHost.ConfigureKestrel(options =>
//{
//	options.Limits.MaxRequestHeadersTotalSize = 65536; // 64 KB
//});

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.ConfigureServiceRegistration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
