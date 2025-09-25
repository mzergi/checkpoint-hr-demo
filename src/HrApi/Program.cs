using HrApi.Options;
using HrDataAccess;
using HrDataAccess.Repositories;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.AutoMapper;
using HrServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


const string ApiScopePolicy = "ApiScope";

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("postgres");

// Add services to the container.
builder.Services.AddDbContext<PostgresHrDbContext>(
    options => options.UseNpgsql(connectionString)
    );

var apiConfig = builder.Configuration.GetSection("ApiConfig").Get<ApiConfig>();

builder.Services.AddSingleton(apiConfig);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = $"CheckPoint.{apiConfig.Name}", Version = "v1" });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Authority"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ApiScopePolicy, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", apiConfig.Scope);
    });
});

// add services
builder.Services.AddScoped<ICandidatesRepository, CandidatesRepository>();
builder.Services.AddScoped<IDocumentTypesRepository, DocumentTypesRepository>();
builder.Services.AddScoped<IEmployeeDeadlinesRepository, EmployeeDeadlinesRepository>();
builder.Services.AddScoped<IEmployeeDocumentsRepository, EmployeeDocumentsRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeSkillsRepository, EmployeeSkillsRepository>();
builder.Services.AddScoped<IEmploymentRepository, EmploymentRepository>();
builder.Services.AddScoped<IProjectHoursRepository, ProjectHoursRepository>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IVacationsRepository, VacationsRepository>();
builder.Services.AddScoped<IVacationTypesRepository, VacationTypesRepository>();
builder.Services.AddScoped<IWorkingHoursRepository, WorkingHoursRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IEmployeeDeadlineService, EmployeeDeadlineService>();
builder.Services.AddScoped<IEmployeeDocumentService, EmployeeDocumentService>();
builder.Services.AddScoped<IEmploymentService, EmploymentService>();
builder.Services.AddScoped<IProjectHourService, ProjectHourService>();
builder.Services.AddScoped<IVacationService, VacationService>();
builder.Services.AddScoped<IVacationTypeService, VacationTypeService>();
builder.Services.AddScoped<IWorkingHourService, WorkingHourService>();

// add automapper
builder.Services.AddAutoMapper(
    typeof(EmployeeProfile),
    typeof(PageProfile),
    typeof(SkillProfile),
    typeof(CandidateProfile),
    typeof(DocumentTypeProfile),
    typeof(EmployeeDeadlineProfile),
    typeof(EmployeeDocumentProfile),
    typeof(EmploymentProfile),
    typeof(ProjectHourProfile),
    typeof(VacationProfile),
    typeof(VacationTypeProfile),
    typeof(WorkingHourProfile)
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

// app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(ApiScopePolicy); });
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();

public partial class Program { }
