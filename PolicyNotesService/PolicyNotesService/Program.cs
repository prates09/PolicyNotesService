using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PolicyNotesDbContext>(options =>
    options.UseInMemoryDatabase("PolicyNotesDb"));


builder.Services.AddScoped<IPolicyNoteRepository, PolicyNoteRepository>();
builder.Services.AddScoped<IPolicyNotesService, PolicyNotesService.Services.PolicyNotesService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/notes", async (PolicyNoteCreateDto dto, IPolicyNotesService service) =>
{
    var created = await service.AddNoteAsync(dto);
    return Results.Created($"/notes/{created.Id}", created);
});


app.MapGet("/notes", async (IPolicyNotesService service) =>
{
    var notes = await service.GetAllNotesAsync();
    return Results.Ok(notes);
});


app.MapGet("/notes/{id:int}", async (int id, IPolicyNotesService service) =>
{
    var note = await service.GetNoteByIdAsync(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

app.Run();


public partial class Program { }
