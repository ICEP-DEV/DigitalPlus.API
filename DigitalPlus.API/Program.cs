using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DigitalPlusDbContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("DigitalPlusDb")));
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
    builder.AllowAnyOrigin()   // Allowing requests from any origin
           .AllowAnyMethod()   // Allowing all HTTP methods (GET, POST, PUT, DELETE, etc.)
           .AllowAnyHeader()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configuere service add contextDb
builder.Services.AddScoped<DigitalPlusDbContext, DigitalPlusDbContext>();
builder.Services.AddScoped<IIRegisterInterface<Mentor>, MentorService>();
builder.Services.AddScoped<IIRegisterInterface<Mentee>, MenteeService>();
builder.Services.AddScoped<IIRegisterInterface<Administrator>, AdminService>();
builder.Services.AddScoped<ICrudInterface<Module>, ModuleService>();
builder.Services.AddScoped<ICrudInterface<Department>, DepartmentService>();
builder.Services.AddScoped<ICrudInterface<Course>, CourseService>();
builder.Services.AddScoped<ICrudInterface<Complaint>, ComplaintsService>();

var app = builder.Build();
app.UseCors("corspolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
