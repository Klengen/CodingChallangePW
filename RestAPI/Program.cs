using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myCorsSettings",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        );
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("_myCorsSettings");


var service = new ProjectService();


app.MapDelete("/project/{id:int}", (int id) =>
{
    var delCount = service.DeleteProject(id);
    return (delCount > 0) ? Results.Ok() : Results.NotFound();
});

app.MapGet("/project/{id:int?}", (int id = -1) =>
{
    if (id <= 0)
    {
        return Results.Ok(service.GetAllProjects());
    }
    else
    {
        var project = service.GetProjectById(id);
        return project == null ? Results.NotFound() : Results.Ok(project);
    }
});

app.MapPost("/project", ([FromBody]Project project) =>
{
    return Results.Ok(service.AddProject(project));
});


app.MapPatch("/project", ([FromBody]Project project) =>
{
    Project oldProject = service.UpdateProject(project);
    if (project == oldProject)
        return Results.NotFound();
    return Results.Ok(oldProject);
});
// This is only to migrate the Data from the file. Just Copy and Paste the file content into a JSON-body in Postman,
// Insomnia or another HTTP-Client and send it to this endpoint.
app.MapPost("/projects", ([FromBody] List<ProjectMigrate> projectMigrateList) =>
{
    List<Project> projects = [];
    foreach (var projectMigrate in projectMigrateList)
    {
        var p = new Project(
            id: projectMigrate.Id,
            city: projectMigrate.City,
            startDate: DateTime.ParseExact(projectMigrate.start_date, "M/d/yyyy", CultureInfo.InvariantCulture), 
            endDate: DateTime.ParseExact(projectMigrate.end_date, "M/d/yyyy", CultureInfo.InvariantCulture), 
            price: projectMigrate.Price, 
            status: projectMigrate.Status,
            color: projectMigrate.Color
            );
        p.Id = projectMigrate.Id;
        projects.Add(p);
    }
    Console.WriteLine(projects[0].City+" "+projects[0].StartDate);
    service.AddListOfProjects(projects);
});

app.Run("http://*:8080");