using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DigitalPlusDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DigitalPlusDb")));

// CORS policy configuration
builder.Services.AddDbContext<DigitalPlusDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DigitalPlusDb")));
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and their interfaces
builder.Services.AddScoped<IIRegisterInterface<Mentor>, MentorService>();
builder.Services.AddScoped<IIRegisterInterface<Mentee>, MenteeService>();
builder.Services.AddScoped<IIRegisterInterface<Administrator>, AdminService>();
builder.Services.AddScoped<ICrudInterface<Module>, ModuleService>();
builder.Services.AddScoped<ICrudInterface<Department>, DepartmentService>();
builder.Services.AddScoped<ICrudInterface<Course>, CourseService>();
builder.Services.AddScoped<ICrudInterface<Complaint>, ComplaintsService>();
builder.Services.AddScoped<AssignModService>();
<<<<<<< HEAD
builder.Services.AddScoped<MentorReportService>();
=======
builder.Services.AddScoped<ICrudInterface<Appointment>, AppointmentService>();
>>>>>>> 9b7414a86c2671df6ca24a1bfe1d14cf870a78a5

var app = builder.Build();

// CORS middleware
app.UseCors("corspolicy");

// Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
