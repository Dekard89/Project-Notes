using FluentValidation;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore.Migrations;
using Note.BLL;
using Note.BLL.DTO;
using NoteApi.Extensions;
using NoteApi.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();



builder.Services.AddCors(options =>
{
    options.AddPolicy("VueOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:8080");
        builder.WithOrigins("http://localhost:443");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowCredentials();
    });
});

builder.AddData()
    .Services.AddScoped<IRepository, NoteStore>();
builder.Services.AddScoped<IValidator<NoteRequest>, NotesValidator>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseCors("VueOrigin");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor| ForwardedHeaders.XForwardedProto
});

app.MapControllers();


app.Run();
