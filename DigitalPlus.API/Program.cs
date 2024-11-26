using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using DigitalPlus.API.hubs;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DigitalPlusDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DigitalPlusDb")));

// CORS policy configuration
builder.Services.AddCors(options => options.AddPolicy("corspolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://localhost:3001") // Allow both ports for local testing
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials(); // Enable credentials
}));


// Register the SendingEmailService for dependency injection
string smtpServer = "smtp.gmail.com"; // Gmail SMTP server
int smtpPort = 587;                     // Port for TLS
string smtpUser = "wementor01@gmail.com"; // Your Gmail address
string smtpPass = "cynh kihn xszj ovjq";     // App password (if using 2FA)

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
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IIMenteeAssignModInteface<MenteeAssignModule>, MenteeAssignModService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();
builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 50 * 1024 * 1024; // 50 MB limit
});

// Configure request size limits
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB limit
});

// Register CustomUserIdProvider
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();



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

app.MapRazorPages();
app.MapHub<CommunicationHub>("/communicationHub");
app.MapHub<ChatBoardHub>("/chatBoardHub"); // New hub for the module chat board


app.Run();
