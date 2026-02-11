 using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), a => a.MigrationsAssembly("Infrastructure")));
builder.Services.AddControllers();
builder.Services.AddScoped<ICourseProgressService,  CourseProgressService>();
builder.Services.AddScoped<ICourseService,  CourseService>();
builder.Services.AddScoped<IModuleService, ModuleService >();
builder.Services.AddScoped<ILessonService, LessonService >();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService >();
builder.Services.AddScoped<IStudentProfileService, StudentProfileService >();
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();