using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.Helpers;
using mks.Services;
using mks.Interfaces;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MKS API",
        Version = "v1"
    });
});

// Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUpdateRoleService, UpdateRoleService>();
builder.Services.AddScoped<IWorkerCategoryService, WorkerCategoryService>();
builder.Services.AddScoped<IWorkerPeriodService, WorkerPeriodService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IAttendanceService , AttendanceService>();
builder.Services.AddScoped<ICreditorService , CreditorService>();
builder.Services.AddScoped<IPayRollService , PayRollService>();
builder.Services.AddScoped<IDeductionService, DeductionService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentBatchService, PaymentBatchService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();