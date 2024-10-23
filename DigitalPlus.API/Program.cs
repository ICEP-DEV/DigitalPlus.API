using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DigitalPlusDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DigitalPlusDb")));

// CORS policy configuration
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()));

// Register the SendingEmailService for dependency injection
string smtpServer = "smtp.gmail.com"; // Gmail SMTP server
int smtpPort = 587;                     // Port for TLS
string smtpUser = "fowardnkuna6@gmail.com"; // Your Gmail address
string smtpPass = "memi yawc olff hglz";     // App password (if using 2FA)

builder.Services.AddScoped<ISendEmail>(provider => new SendingEmailService(smtpServer, smtpPort, smtpUser, smtpPass));

// Register other services and their interfaces
builder.Services.AddScoped<IIRegisterInterface<Mentor>, MentorService>();
builder.Services.AddScoped<IIRegisterInterface<Mentee>, MenteeService>();
builder.Services.AddScoped<IIRegisterInterface<Administrator>, AdminService>();
builder.Services.AddScoped<ICrudInterface<Module>, ModuleService>();
builder.Services.AddScoped<ICrudInterface<Department>, DepartmentService>();
builder.Services.AddScoped<ICrudInterface<Course>, CourseService>();
builder.Services.AddScoped<ICrudInterface<Complaint>, ComplaintsService>();
builder.Services.AddScoped<AssignModuleService>();
builder.Services.AddScoped<MentorReportService>();
builder.Services.AddScoped<ICrudInterface<Appointment>, AppointmentService>();
builder.Services.AddScoped<AdminDashboardService>();
builder.Services.AddScoped<IAssignModService<AssignMod>, AssignModuleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
