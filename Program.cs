using Microsoft.EntityFrameworkCore;
using pom_api.Context;
using pom_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeProjectDB>(opt => opt.UseInMemoryDatabase("EmployeeProjectDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.MapGet("/", () => "Hello World!");


app.MapGet("/employees", async (EmployeeProjectDB db) =>
    await db.EmployeeProjects.Select(ep => ep.EmployeeName)
        .Distinct()
        .ToListAsync());
    

app.MapGet("/projects", async (EmployeeProjectDB db) =>
     await db.EmployeeProjects.Select(ep => ep.ProjectName)
        .Distinct()
        .ToListAsync());


app.MapPost("/add-data", async (EmployeeProjects EmployeeProject, EmployeeProjectDB db) =>
{
    db.EmployeeProjects.Add(EmployeeProject);
    await db.SaveChangesAsync();

    return Results.Ok(EmployeeProject);
});

app.MapGet("/get-report", async (EmployeeProjectDB db, DateTime startTime, DateTime endTime, string employeeName, string projectName) =>
{
    var filteredProjects = await db.EmployeeProjects
        .Where(ep => ep.StartTime >= startTime && ep.EndTime <= endTime)
        .ToListAsync();

    if (!string.IsNullOrEmpty(employeeName))
    {
        filteredProjects = filteredProjects.Where(ep => ep.EmployeeName == employeeName).ToList();
    }

    if (!string.IsNullOrEmpty(projectName))
    {
        filteredProjects = filteredProjects.Where(ep => ep.ProjectName == projectName).ToList();
    }

    return filteredProjects;
});

app.Run();
